// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared\dxgicommon.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop
{
    public enum DXGI_COLOR_SPACE_TYPE : uint
    {
        DXGI_COLOR_SPACE_RGB_FULL_G22_NONE_P709 = 0,

        DXGI_COLOR_SPACE_RGB_FULL_G10_NONE_P709 = 1,

        DXGI_COLOR_SPACE_RGB_STUDIO_G22_NONE_P709 = 2,

        DXGI_COLOR_SPACE_RGB_STUDIO_G22_NONE_P2020 = 3,

        DXGI_COLOR_SPACE_RESERVED = 4,

        DXGI_COLOR_SPACE_YCBCR_FULL_G22_NONE_P709_X601 = 5,

        DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P601 = 6,

        DXGI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P601 = 7,

        DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P709 = 8,

        DXGI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P709 = 9,

        DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_LEFT_P2020 = 10,

        DXGI_COLOR_SPACE_YCBCR_FULL_G22_LEFT_P2020 = 11,

        DXGI_COLOR_SPACE_RGB_FULL_G2084_NONE_P2020 = 12,

        DXGI_COLOR_SPACE_YCBCR_STUDIO_G2084_LEFT_P2020 = 13,

        DXGI_COLOR_SPACE_RGB_STUDIO_G2084_NONE_P2020 = 14,

        DXGI_COLOR_SPACE_YCBCR_STUDIO_G22_TOPLEFT_P2020 = 15,

        DXGI_COLOR_SPACE_YCBCR_STUDIO_G2084_TOPLEFT_P2020 = 16,

        DXGI_COLOR_SPACE_RGB_FULL_G22_NONE_P2020 = 17,

        DXGI_COLOR_SPACE_YCBCR_STUDIO_GHLG_TOPLEFT_P2020 = 18,

        DXGI_COLOR_SPACE_YCBCR_FULL_GHLG_TOPLEFT_P2020 = 19,

        DXGI_COLOR_SPACE_CUSTOM = 0xFFFFFFFF
    }
}
