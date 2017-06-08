// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared\dxgi.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TerraFX.Interop.DXGI
{
    [Guid("4AE63092-6327-4C1B-80AE-BFE12EA32B86")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [SuppressUnmanagedCodeSecurity]
    public interface IDXGISurface1 : IDXGISurface
    {
        #region IDXGIObject
        new void SetPrivateData([In] ref Guid Name, [In] uint DataSize, [In] IntPtr pData);

        new void SetPrivateDataInterface([In] ref Guid Name, [MarshalAs(UnmanagedType.IUnknown), In] object pUnknown);

        new void GetPrivateData([In] ref Guid Name, [In, Out] ref uint pDataSize, [Out] IntPtr pData);

        new IntPtr GetParent([In] ref Guid riid);
        #endregion

        #region IDXGIDeviceSubObject
        new IntPtr GetDevice([In] ref Guid riid);
        #endregion

        #region IDXGISurface
        new void GetDesc(out DXGI_SURFACE_DESC pDesc);

        new void Map(out DXGI_MAPPED_RECT pLockedRect, [In] DXGI_MAP_FLAG MapFlags);

        new void Unmap();
        #endregion

        #region Methods
        void GetDC([In] int Discard, [Out] IntPtr phdc);

        void ReleaseDC([In] ref RECT pDirtyRect);
        #endregion
    }
}
