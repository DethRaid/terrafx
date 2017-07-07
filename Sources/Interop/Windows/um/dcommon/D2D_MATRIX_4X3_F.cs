// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from um\dcommon.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.InteropServices;

namespace TerraFX.Interop
{
    /// <summary>Represents a 4-by-3 matrix.</summary>
    [StructLayout(LayoutKind.Explicit)]
    unsafe public /* blittable */ struct D2D_MATRIX_4X3_F
    {
        #region Fields
        #region struct
        [FieldOffset(0)]
        public FLOAT _11;

        [FieldOffset(4)]
        public FLOAT _12;

        [FieldOffset(8)]
        public FLOAT _13;

        [FieldOffset(12)]
        public FLOAT _21;

        [FieldOffset(16)]
        public FLOAT _22;

        [FieldOffset(20)]
        public FLOAT _23;

        [FieldOffset(24)]
        public FLOAT _31;

        [FieldOffset(28)]
        public FLOAT _32;

        [FieldOffset(32)]
        public FLOAT _33;

        [FieldOffset(36)]
        public FLOAT _41;

        [FieldOffset(40)]
        public FLOAT _42;

        [FieldOffset(44)]
        public FLOAT _43;
        #endregion

        [FieldOffset(0)]
        public _m_e__FixedBuffer m;
        #endregion

        #region Structs
        public /* blittable */ struct _m_e__FixedBuffer
        {
            #region Fields
            #region 0
            public FLOAT _0_0;

            public FLOAT _0_1;

            public FLOAT _0_2;
            #endregion

            #region 1
            public FLOAT _1_0;

            public FLOAT _1_1;

            public FLOAT _1_2;
            #endregion

            #region 2
            public FLOAT _2_0;

            public FLOAT _2_1;

            public FLOAT _2_2;
            #endregion

            #region 3
            public FLOAT _3_0;

            public FLOAT _3_1;

            public FLOAT _3_2;
            #endregion
            #endregion
        }
        #endregion
    }
}