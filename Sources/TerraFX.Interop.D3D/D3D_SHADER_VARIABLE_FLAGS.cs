// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from um\d3dcommon.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;

namespace TerraFX.Interop
{
    [Flags]
    public enum D3D_SHADER_VARIABLE_FLAGS
    {
        NONE = 0,

        USERPACKED = 1,

        USED = 2,

        INTERFACE_POINTER = 4,

        INTERFACE_PARAMETER = 8,

        FORCE_DWORD = 0x7FFFFFFF
    }
}