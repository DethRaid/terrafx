// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um\d2d1.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop
{
    /// <summary>Differentiates which of the two possible arcs could match the given arc parameters.</summary>
    public enum D2D1_ARC_SIZE : uint
    {
        D2D1_ARC_SIZE_SMALL = 0,

        D2D1_ARC_SIZE_LARGE = 1,

        D2D1_ARC_SIZE_FORCE_DWORD = 0xFFFFFFFF
    }
}