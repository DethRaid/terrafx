// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared\dxgi1_3.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Security;
using TerraFX.Utilities;
using static TerraFX.Utilities.InteropUtilities;

namespace TerraFX.Interop
{
    [Guid("2633066B-4514-4C7A-8FD8-12EA98059D18")]
    [Unmanaged]
    public unsafe struct IDXGIDecodeSwapChain
    {
        #region Fields
        public readonly Vtbl* lpVtbl;
        #endregion

        #region IUnknown Delegates
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _QueryInterface(
            [In] IDXGIDecodeSwapChain* This,
            [In, NativeTypeName("REFIID")] Guid* riid,
            [Out] void** ppvObject
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("ULONG")]
        public /* static */ delegate uint _AddRef(
            [In] IDXGIDecodeSwapChain* This
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("ULONG")]
        public /* static */ delegate uint _Release(
            [In] IDXGIDecodeSwapChain* This
        );
        #endregion

        #region Delegates
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _PresentBuffer(
            [In] IDXGIDecodeSwapChain* This,
            [In, NativeTypeName("UINT")] uint BufferToPresent,
            [In, NativeTypeName("UINT")] uint SyncInterval,
            [In, NativeTypeName("UINT")] uint Flags
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _SetSourceRect(
            [In] IDXGIDecodeSwapChain* This,
            [In] RECT* pRect
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _SetTargetRect(
            [In] IDXGIDecodeSwapChain* This,
            [In] RECT* pRect
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _SetDestSize(
            [In] IDXGIDecodeSwapChain* This,
            [In, NativeTypeName("UINT")] uint Width,
            [In, NativeTypeName("UINT")] uint Height
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _GetSourceRect(
            [In] IDXGIDecodeSwapChain* This,
            [Out] RECT* pRect
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _GetTargetRect(
            [In] IDXGIDecodeSwapChain* This,
            [Out] RECT* pRect
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _GetDestSize(
            [In] IDXGIDecodeSwapChain* This,
            [Out, NativeTypeName("UINT")] uint* pWidth,
            [Out, NativeTypeName("UINT")] uint* pHeight
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: NativeTypeName("HRESULT")]
        public /* static */ delegate int _SetColorSpace(
            [In] IDXGIDecodeSwapChain* This,
            [In] DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS ColorSpace
        );

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.StdCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        public /* static */ delegate DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS _GetColorSpace(
            [In] IDXGIDecodeSwapChain* This
        );
        #endregion

        #region IUnknown Methods
        [return: NativeTypeName("HRESULT")]
        public int QueryInterface(
            [In, NativeTypeName("REFIID")] Guid* riid,
            [Out] void** ppvObject
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_QueryInterface>(lpVtbl->QueryInterface)(
                    This,
                    riid,
                    ppvObject
                );
            }
        }

        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_AddRef>(lpVtbl->AddRef)(
                    This
                );
            }
        }

        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_Release>(lpVtbl->Release)(
                    This
                );
            }
        }
        #endregion

        #region Methods
        [return: NativeTypeName("HRESULT")]
        public int PresentBuffer(
            [In, NativeTypeName("UINT")] uint BufferToPresent,
            [In, NativeTypeName("UINT")] uint SyncInterval,
            [In, NativeTypeName("UINT")] uint Flags
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_PresentBuffer>(lpVtbl->PresentBuffer)(
                    This,
                    BufferToPresent,
                    SyncInterval,
                    Flags
                );
            }
        }

        [return: NativeTypeName("HRESULT")]
        public int SetSourceRect(
            [In] RECT* pRect
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_SetSourceRect>(lpVtbl->SetSourceRect)(
                    This,
                    pRect
                );
            }
        }

        [return: NativeTypeName("HRESULT")]
        public int SetTargetRect(
            [In] RECT* pRect
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_SetTargetRect>(lpVtbl->SetTargetRect)(
                    This,
                    pRect
                );
            }
        }

        [return: NativeTypeName("HRESULT")]
        public int SetDestSize(
            [In, NativeTypeName("UINT")] uint Width,
            [In, NativeTypeName("UINT")] uint Height
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_SetDestSize>(lpVtbl->SetDestSize)(
                    This,
                    Width,
                    Height
                );
            }
        }

        [return: NativeTypeName("HRESULT")]
        public int GetSourceRect(
            [Out] RECT* pRect
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_GetSourceRect>(lpVtbl->GetSourceRect)(
                    This,
                    pRect
                );
            }
        }

        [return: NativeTypeName("HRESULT")]
        public int GetTargetRect(
            [Out] RECT* pRect
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_GetTargetRect>(lpVtbl->GetTargetRect)(
                    This,
                    pRect
                );
            }
        }

        [return: NativeTypeName("HRESULT")]
        public int GetDestSize(
            [Out, NativeTypeName("UINT")] uint* pWidth,
            [Out, NativeTypeName("UINT")] uint* pHeight
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_GetDestSize>(lpVtbl->GetDestSize)(
                    This,
                    pWidth,
                    pHeight
                );
            }
        }

        [return: NativeTypeName("HRESULT")]
        public int SetColorSpace(
            [In] DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS ColorSpace
        )
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_SetColorSpace>(lpVtbl->SetColorSpace)(
                    This,
                    ColorSpace
                );
            }
        }

        public DXGI_MULTIPLANE_OVERLAY_YCbCr_FLAGS GetColorSpace()
        {
            fixed (IDXGIDecodeSwapChain* This = &this)
            {
                return MarshalFunction<_GetColorSpace>(lpVtbl->GetColorSpace)(
                    This
                );
            }
        }
        #endregion

        #region Structs
        [Unmanaged]
        public struct Vtbl
        {
            #region IUnknown Fields
            public IntPtr QueryInterface;

            public IntPtr AddRef;

            public IntPtr Release;
            #endregion

            #region Fields
            public IntPtr PresentBuffer;

            public IntPtr SetSourceRect;

            public IntPtr SetTargetRect;

            public IntPtr SetDestSize;

            public IntPtr GetSourceRect;

            public IntPtr GetTargetRect;

            public IntPtr GetDestSize;

            public IntPtr SetColorSpace;

            public IntPtr GetColorSpace;
            #endregion
        }
        #endregion
    }
}