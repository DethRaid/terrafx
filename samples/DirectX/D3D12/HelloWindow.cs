// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from D3D12HelloWindow.h in https://github.com/Microsoft/DirectX-Graphics-Samples
// Original source is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System;
using TerraFX.Interop;
using TerraFX.Utilities;
using static TerraFX.Interop.D3D_FEATURE_LEVEL;
using static TerraFX.Interop.D3D12;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;
using static TerraFX.Interop.D3D12_COMMAND_QUEUE_FLAGS;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_FLAGS;
using static TerraFX.Interop.D3D12_DESCRIPTOR_HEAP_TYPE;
using static TerraFX.Interop.D3D12_FENCE_FLAGS;
using static TerraFX.Interop.D3D12_RESOURCE_BARRIER_FLAGS;
using static TerraFX.Interop.D3D12_RESOURCE_BARRIER_TYPE;
using static TerraFX.Interop.D3D12_RESOURCE_STATES;
using static TerraFX.Interop.DXGI;
using static TerraFX.Interop.DXGI_FORMAT;
using static TerraFX.Interop.DXGI_SWAP_EFFECT;
using static TerraFX.Interop.Kernel32;
using static TerraFX.Interop.Windows;
using static TerraFX.Samples.DirectX.D3D12.DXSampleHelper;
using static TerraFX.Utilities.ExceptionUtilities;

namespace TerraFX.Samples.DirectX.D3D12
{
    public unsafe class HelloWindow : DXSample
    {
        #region Constants
        private const uint FrameCount = 2;
        #endregion

        #region Fields
        // Pipeline objects
        private IDXGISwapChain3* _swapChain;
        private ID3D12Device* _device;
        private RenderTargets_e__FixedBuffer _renderTargets;
        private ID3D12CommandAllocator* _commandAllocator;
        private ID3D12CommandQueue* _commandQueue;
        private ID3D12DescriptorHeap* _rtvHeap;
        private ID3D12PipelineState* _pipelineState;
        private ID3D12GraphicsCommandList* _commandList;
        private uint _rtvDescriptorSize;

        // Synchronization objects.
        private uint _frameIndex;
        private IntPtr _fenceEvent;
        private ID3D12Fence* _fence;
        private ulong _fenceValue;
        #endregion

        #region Constructors
        public HelloWindow(uint width, uint height, string name)
            : base(width, height, name)
        {
        }
        #endregion

        #region Methods
        public override void OnInit()
        {
            LoadPipeline();
            LoadAssets();
        }

        // Update frame-based values.
        public override void OnUpdate()
        {
        }

        // Render the scene.
        public override void OnRender()
        {
            // Record all the commands we need to render the scene into the command list.
            PopulateCommandList();

            // Execute the command list.
            var ppCommandLists = stackalloc ID3D12CommandList*[1];
            {
                ppCommandLists[0] = (ID3D12CommandList*)_commandList;
            }
            _commandQueue->ExecuteCommandLists(1, ppCommandLists);

            // Present the frame.
            ThrowIfFailed(nameof(IDXGISwapChain3.Present), _swapChain->Present(1, 0));

            WaitForPreviousFrame();
        }

        public override void OnDestroy()
        {
            // Ensure that the GPU is no longer referencing resources that are about to be
            // cleaned up by the destructor.
            WaitForPreviousFrame();

            CloseHandle(_fenceEvent);
        }

        // Load the rendering pipeline dependencies.
        private void LoadPipeline()
        {
            Guid iid;
            ID3D12Debug* debugController = null;
            IDXGIFactory4* factory = null;
            IDXGIAdapter* adapter = null;
            IDXGISwapChain1* swapChain = null;

            try
            {
                var dxgiFactoryFlags = 0u;

#if DEBUG
                // Enable the debug layer (requires the Graphics Tools "optional feature").
                // NOTE: Enabling the debug layer after device creation will invalidate the active device.
                {
                    iid = IID_ID3D12Debug;
                    if (SUCCEEDED(D3D12GetDebugInterface(&iid, (void**)&debugController)))
                    {
                        debugController->EnableDebugLayer();

                        // Enable additional debug layers.
                        dxgiFactoryFlags |= DXGI_CREATE_FACTORY_DEBUG;
                    }
                }
#endif

                iid = IID_IDXGIFactory4;
                ThrowIfFailed(nameof(CreateDXGIFactory2), CreateDXGIFactory2(dxgiFactoryFlags, &iid, (void**)&factory));

                if (_useWarpDevice)
                {
                    iid = IID_IDXGIAdapter;
                    ThrowIfFailed(nameof(IDXGIFactory4.EnumWarpAdapter), factory->EnumWarpAdapter(&iid, (void**)&adapter));
                }
                else
                {
                    adapter = GetHardwareAdapter(factory);
                }

                fixed (ID3D12Device** device = &_device)
                {
                    iid = IID_ID3D12Device;
                    ThrowIfFailed(nameof(D3D12CreateDevice), D3D12CreateDevice((IUnknown*)adapter, D3D_FEATURE_LEVEL_11_0, &iid, (void**)device));
                }

                // Describe and create the command queue.
                var queueDesc = new D3D12_COMMAND_QUEUE_DESC {
                    Flags = D3D12_COMMAND_QUEUE_FLAG_NONE,
                    Type = D3D12_COMMAND_LIST_TYPE_DIRECT
                };

                fixed (ID3D12CommandQueue** commandQueue = &_commandQueue)
                {
                    iid = IID_ID3D12CommandQueue;
                    ThrowIfFailed(nameof(ID3D12Device.CreateCommandQueue), _device->CreateCommandQueue(&queueDesc, &iid, (void**)commandQueue));
                }

                // Describe and create the swap chain.
                var swapChainDesc = new DXGI_SWAP_CHAIN_DESC1 {
                    BufferCount = FrameCount,
                    Width = _width,
                    Height = _height,
                    Format = DXGI_FORMAT_R8G8B8A8_UNORM,
                    BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT,
                    SwapEffect = DXGI_SWAP_EFFECT_FLIP_DISCARD,
                    SampleDesc = new DXGI_SAMPLE_DESC {
                        Count = 1
                    }
                };

                ThrowIfFailed(nameof(IDXGIFactory4.CreateSwapChainForHwnd), factory->CreateSwapChainForHwnd(
                    (IUnknown*)_commandQueue,         // Swap chain needs the queue so that it can force a flush on it.
                    Win32Application.Hwnd,
                    &swapChainDesc,
                    null,
                    null,
                    &swapChain
                ));

                // This sample does not support fullscreen transitions.
                ThrowIfFailed(nameof(IDXGIFactory4.MakeWindowAssociation), factory->MakeWindowAssociation(Win32Application.Hwnd, DXGI_MWA_NO_ALT_ENTER));

                fixed (IDXGISwapChain3** swapChain3 = &_swapChain)
                {
                    iid = IID_IDXGISwapChain3;
                    ThrowIfFailed(nameof(IDXGISwapChain1.QueryInterface), swapChain->QueryInterface(&iid, (void**)swapChain3));
                    _frameIndex = _swapChain->GetCurrentBackBufferIndex();
                }

                // Create descriptor heaps.
                {
                    // Describe and create a render target view (RTV) descriptor heap.
                    var rtvHeapDesc = new D3D12_DESCRIPTOR_HEAP_DESC {
                        NumDescriptors = FrameCount,
                        Type = D3D12_DESCRIPTOR_HEAP_TYPE_RTV,
                        Flags = D3D12_DESCRIPTOR_HEAP_FLAG_NONE
                    };

                    fixed (ID3D12DescriptorHeap** rtvHeap = &_rtvHeap)
                    {
                        iid = IID_ID3D12DescriptorHeap;
                        ThrowIfFailed(nameof(ID3D12Device.CreateDescriptorHeap), _device->CreateDescriptorHeap(&rtvHeapDesc, &iid, (void**)rtvHeap));
                    }

                    _rtvDescriptorSize = _device->GetDescriptorHandleIncrementSize(D3D12_DESCRIPTOR_HEAP_TYPE_RTV);
                }

                // Create frame resources.
                {
                    D3D12_CPU_DESCRIPTOR_HANDLE rtvHandle;
                    _rtvHeap->GetCPUDescriptorHandleForHeapStart(&rtvHandle);

                    // Create a RTV for each frame.
                    iid = IID_ID3D12Resource;

                    for (var n = 0u; n < FrameCount; n++)
                    {
                        ID3D12Resource* renderTarget;
                        {
                            ThrowIfFailed(nameof(IDXGISwapChain3.GetBuffer), _swapChain->GetBuffer(n, &iid, (void**)&renderTarget));
                            _device->CreateRenderTargetView(renderTarget, null, rtvHandle);
                            rtvHandle.ptr += _rtvDescriptorSize;
                        }
                        _renderTargets[unchecked((int)n)] = renderTarget;
                    }
                }

                fixed (ID3D12CommandAllocator** commandAllocator = &_commandAllocator)
                {
                    iid = IID_ID3D12CommandAllocator;
                    ThrowIfFailed(nameof(ID3D12Device.CreateRenderTargetView), _device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT, &iid, (void**)commandAllocator));
                }
            }
            finally
            {
                if (debugController != null)
                {
                    debugController->Release();
                }

                if (factory != null)
                {
                    factory->Release();
                }

                if (adapter != null)
                {
                    adapter->Release();
                }

                if (swapChain != null)
                {
                    swapChain->Release();
                }
            }
        }

        // Load the sample assets.
        private void LoadAssets()
        {
            // Create the command list.
            fixed (ID3D12GraphicsCommandList** commandList = &_commandList)
            {
                var iid = IID_ID3D12GraphicsCommandList;
                ThrowIfFailed(nameof(ID3D12Device.CreateCommandList), _device->CreateCommandList(0, D3D12_COMMAND_LIST_TYPE_DIRECT, _commandAllocator, null, &iid, (void**)commandList));
            }

            // Command lists are created in the recording state, but there is nothing
            // to record yet. The main loop expects it to be closed, so close it now.
            ThrowIfFailed(nameof(ID3D12GraphicsCommandList.Close), _commandList->Close());

            // Create synchronization objects.
            {
                fixed (ID3D12Fence** fence = &_fence)
                {
                    var iid = IID_ID3D12Fence;
                    ThrowIfFailed(nameof(ID3D12Device.CreateFence), _device->CreateFence(0, D3D12_FENCE_FLAG_NONE, &iid, (void**)fence));
                    _fenceValue = 1;
                }

                // Create an event handle to use for frame synchronization.
                _fenceEvent = CreateEvent(null, FALSE, FALSE, null);
                if (_fenceEvent == IntPtr.Zero)
                {
                    ThrowExternalExceptionForLastHRESULT(nameof(CreateEvent));
                }
            }
        }

        private void PopulateCommandList()
        {
            // Command list allocators can only be reset when the associated
            // command lists have finished execution on the GPU; apps should use
            // fences to determine GPU execution progress.
            ThrowIfFailed(nameof(ID3D12CommandAllocator.Reset), _commandAllocator->Reset());

            // However, when ExecuteCommandList() is called on a particular command
            // list, that command list can then be reset at any time and must be before
            // re-recording.
            ThrowIfFailed(nameof(ID3D12GraphicsCommandList.Reset), _commandList->Reset(_commandAllocator, _pipelineState));

            // Indicate that the back buffer will be used as a render target.
            var barrier = new D3D12_RESOURCE_BARRIER {
                Type = D3D12_RESOURCE_BARRIER_TYPE_TRANSITION,
                Flags = D3D12_RESOURCE_BARRIER_FLAG_NONE,
                Transition = new D3D12_RESOURCE_TRANSITION_BARRIER {
                    pResource = _renderTargets[unchecked((int)_frameIndex)],
                    StateBefore = D3D12_RESOURCE_STATE_PRESENT,
                    StateAfter = D3D12_RESOURCE_STATE_RENDER_TARGET,
                    Subresource = D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES
                }
            };
            _commandList->ResourceBarrier(1, &barrier);

            D3D12_CPU_DESCRIPTOR_HANDLE rtvHandle;
            _rtvHeap->GetCPUDescriptorHandleForHeapStart(&rtvHandle);
            rtvHandle.ptr += _frameIndex * _rtvDescriptorSize;

            // Record commands.
            var clearColor = stackalloc float[4];
            {
                clearColor[0] = 0.0f;
                clearColor[1] = 0.2f;
                clearColor[2] = 0.4f;
                clearColor[3] = 1.0f;
            }
            _commandList->ClearRenderTargetView(rtvHandle, clearColor, 0, null);

            // Indicate that the back buffer will now be used to present.
            {
                barrier.Transition.StateBefore = D3D12_RESOURCE_STATE_RENDER_TARGET;
                barrier.Transition.StateAfter = D3D12_RESOURCE_STATE_PRESENT;
            }
            _commandList->ResourceBarrier(1, &barrier);

            ThrowIfFailed(nameof(ID3D12GraphicsCommandList.Close), _commandList->Close());
        }

        private void WaitForPreviousFrame()
        {
            // WAITING FOR THE FRAME TO COMPLETE BEFORE CONTINUING IS NOT BEST PRACTICE.
            // This is code implemented as such for simplicity. The D3D12HelloFrameBuffering
            // sample illustrates how to use fences for efficient resource usage and to
            // maximize GPU utilization.

            // Signal and increment the fence value.
            var fence = _fenceValue;
            ThrowIfFailed(nameof(ID3D12CommandQueue.Signal), _commandQueue->Signal(_fence, fence));
            _fenceValue++;

            // Wait until the previous frame is finished.
            if (_fence->GetCompletedValue() < fence)
            {
                ThrowIfFailed(nameof(ID3D12Fence.SetEventOnCompletion), _fence->SetEventOnCompletion(fence, _fenceEvent));
                WaitForSingleObject(_fenceEvent, INFINITE);
            }

            _frameIndex = _swapChain->GetCurrentBackBufferIndex();
        }

        protected override void Dispose(bool isDisposing)
        {
            var swapChain = _swapChain;

            if (swapChain != null)
            {
                _swapChain = null;
                swapChain->Release();
            }

            var device = _device;

            if (device != null)
            {
                _device = null;
                device->Release();
            }

            for (var index = 0; index < FrameCount; index++)
            {
                var renderTarget = _renderTargets[index];

                if (renderTarget != null)
                {
                    _renderTargets[index] = null;
                    renderTarget->Release();
                }
            }

            var commandAllocator = _commandAllocator;

            if (commandAllocator != null)
            {
                _commandAllocator = null;
                commandAllocator->Release();
            }

            var commandQueue = _commandQueue;

            if (commandQueue != null)
            {
                _commandQueue = null;
                commandQueue->Release();
            }

            var rtvHeap = _rtvHeap;

            if (rtvHeap != null)
            {
                _rtvHeap = null;
                rtvHeap->Release();
            }

            var pipelineState = _pipelineState;

            if (pipelineState != null)
            {
                _pipelineState = null;
                pipelineState->Release();
            }

            var commandList = _commandList;

            if (commandList != null)
            {
                _commandList = null;
                commandList->Release();
            }

            var fence = _fence;

            if (fence != null)
            {
                _fence = null;
                fence->Release();
            }

            base.Dispose(isDisposing);
        }
        #endregion

        #region Structs
        [Unmanaged]
        private unsafe struct RenderTargets_e__FixedBuffer
        {
            #region Fields
#pragma warning disable CS0649
            public ID3D12Resource* e0;

            public ID3D12Resource* e1;
#pragma warning restore CS0649
            #endregion

            #region Properties
            public ID3D12Resource* this[int index]
            {
                get
                {
                    fixed (ID3D12Resource** e = &e0)
                    {
                        return e[index];
                    }
                }

                set
                {
                    fixed (ID3D12Resource** e = &e0)
                    {
                        e[index] = value;
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}
