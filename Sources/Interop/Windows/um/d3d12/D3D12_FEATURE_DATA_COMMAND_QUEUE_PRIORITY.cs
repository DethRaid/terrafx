// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from um\d3d12.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

namespace TerraFX.Interop
{
    public /* blittable */ struct D3D12_FEATURE_DATA_COMMAND_QUEUE_PRIORITY
    {
        #region Fields
        public D3D12_COMMAND_LIST_TYPE CommandListType;

        public UINT Priority;

        public BOOL PriorityForTypeIsSupported;
        #endregion
    }
}