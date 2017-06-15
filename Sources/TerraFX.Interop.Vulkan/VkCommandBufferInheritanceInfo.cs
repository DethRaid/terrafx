// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License MIT. See License.md in the repository root for more information.

// Ported from src\spec\vk.xml in the Vulkan-Docs repository for tag v1.0.51-core
// Original source is Copyright © 2015-2017 The Khronos Group Inc.

namespace TerraFX.Interop
{
    unsafe public struct VkCommandBufferInheritanceInfo
    {
        #region Fields
        public VkStructureType sType;

        public void* pNext;

        public VkRenderPass renderPass;

        public uint subpass;

        public VkFramebuffer framebuffer;

        public VkBool32 occlusionQueryEnable;

        public VkQueryControlFlags queryFlags;

        public VkQueryPipelineStatisticFlags pipelineStatistics;
        #endregion
    }
}
