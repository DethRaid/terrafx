// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um\d2d1effects.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop
{
    /// <summary>The enumeration of the Point-Diffuse effect's top level properties.</summary>
    public enum D2D1_POINTDIFFUSE_PROP : uint
    {
        /// <summary>Property Name: "LightPosition" Property Type: D2D1_VECTOR_3F</summary>
        D2D1_POINTDIFFUSE_PROP_LIGHT_POSITION = 0,

        /// <summary>Property Name: "DiffuseConstant" Property Type: FLOAT</summary>
        D2D1_POINTDIFFUSE_PROP_DIFFUSE_CONSTANT = 1,

        /// <summary>Property Name: "SurfaceScale" Property Type: FLOAT</summary>
        D2D1_POINTDIFFUSE_PROP_SURFACE_SCALE = 2,

        /// <summary>Property Name: "Color" Property Type: D2D1_VECTOR_3F</summary>
        D2D1_POINTDIFFUSE_PROP_COLOR = 3,

        /// <summary>Property Name: "KernelUnitLength" Property Type: D2D1_VECTOR_2F</summary>
        D2D1_POINTDIFFUSE_PROP_KERNEL_UNIT_LENGTH = 4,

        /// <summary>Property Name: "ScaleMode" Property Type: D2D1_POINTDIFFUSE_SCALE_MODE</summary>
        D2D1_POINTDIFFUSE_PROP_SCALE_MODE = 5,

        D2D1_POINTDIFFUSE_PROP_FORCE_DWORD = 0xFFFFFFFF
    }
}