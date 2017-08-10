// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um\dwrite.h in the Windows SDK for Windows 10.0.15063.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TerraFX.Interop
{
    /// <summary>Analyzes various text properties for complex script processing.</summary>
    [Guid("B7E6163E-7F46-43B4-84B3-E4E6249C365D")]
    unsafe public /* blittable */ struct IDWriteTextAnalyzer
    {
        #region Fields
        public readonly Vtbl* lpVtbl;
        #endregion

        #region Delegates
        /// <summary>Analyzes a text range for script boundaries, reading text attributes from the source and reporting the Unicode script ID to the sink callback SetScript.</summary>
        /// <param name="analysisSource">Source object to analyze.</param>
        /// <param name="textPosition">Starting position within the source object.</param>
        /// <param name="textLength">Length to analyze.</param>
        /// <param name="analysisSink">Callback object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int AnalyzeScript(
            [In] IDWriteTextAnalyzer* This,
            [In] IDWriteTextAnalysisSource* analysisSource,
            [In, ComAliasName("UINT32")] uint textPosition,
            [In, ComAliasName("UINT32")] uint textLength,
            [In] IDWriteTextAnalysisSink* analysisSink
        );

        /// <summary>Analyzes a text range for script directionality, reading attributes from the source and reporting levels to the sink callback SetBidiLevel.</summary>
        /// <param name="analysisSource">Source object to analyze.</param>
        /// <param name="textPosition">Starting position within the source object.</param>
        /// <param name="textLength">Length to analyze.</param>
        /// <param name="analysisSink">Callback object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        /// <remarks> While the function can handle multiple paragraphs, the text range should not arbitrarily split the middle of paragraphs. Otherwise the returned levels may be wrong, since the Bidi algorithm is meant to apply to the paragraph as a whole.</remarks>
        /// <remarks> Embedded control codes (LRE/LRO/RLE/RLO/PDF) are taken into account.</remarks>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int AnalyzeBidi(
            [In] IDWriteTextAnalyzer* This,
            [In] IDWriteTextAnalysisSource* analysisSource,
            [In, ComAliasName("UINT32")] uint textPosition,
            [In, ComAliasName("UINT32")] uint textLength,
            [In] IDWriteTextAnalysisSink* analysisSink
        );

        /// <summary>Analyzes a text range for spans where number substitution is applicable, reading attributes from the source and reporting substitutable ranges to the sink callback SetNumberSubstitution.</summary>
        /// <param name="analysisSource">Source object to analyze.</param>
        /// <param name="textPosition">Starting position within the source object.</param>
        /// <param name="textLength">Length to analyze.</param>
        /// <param name="analysisSink">Callback object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        /// <remarks> While the function can handle multiple ranges of differing number substitutions, the text ranges should not arbitrarily split the middle of numbers. Otherwise it will treat the numbers separately and will not translate any intervening punctuation.</remarks>
        /// <remarks> Embedded control codes (LRE/LRO/RLE/RLO/PDF) are taken into account.</remarks>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int AnalyzeNumberSubstitution(
            [In] IDWriteTextAnalyzer* This,
            [In] IDWriteTextAnalysisSource* analysisSource,
            [In, ComAliasName("UINT32")] uint textPosition,
            [In, ComAliasName("UINT32")] uint textLength,
            [In] IDWriteTextAnalysisSink* analysisSink
        );

        /// <summary>Analyzes a text range for potential breakpoint opportunities, reading attributes from the source and reporting breakpoint opportunities to the sink callback SetLineBreakpoints.</summary>
        /// <param name="analysisSource">Source object to analyze.</param>
        /// <param name="textPosition">Starting position within the source object.</param>
        /// <param name="textLength">Length to analyze.</param>
        /// <param name="analysisSink">Callback object.</param>
        /// <returns>Standard HRESULT error code.</returns>
        /// <remarks> While the function can handle multiple paragraphs, the text range should not arbitrarily split the middle of paragraphs, unless the given text span is considered a whole unit. Otherwise the returned properties for the first and last characters will inappropriately allow breaks.</remarks>
        /// <remarks> Special cases include the first, last, and surrogate characters. Any text span is treated as if adjacent to inline objects on either side. So the rules with contingent-break opportunities are used, where the edge between text and inline objects is always treated as a potential break opportunity, dependent on any overriding rules of the adjacent objects to prohibit or force the break (see Unicode TR #14). Surrogate pairs never break between.</remarks>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int AnalyzeLineBreakpoints(
            [In] IDWriteTextAnalyzer* This,
            [In] IDWriteTextAnalysisSource* analysisSource,
            [In, ComAliasName("UINT32")] uint textPosition,
            [In, ComAliasName("UINT32")] uint textLength,
            [In] IDWriteTextAnalysisSink* analysisSink
        );

        /// <summary>Parses the input text string and maps it to the set of glyphs and associated glyph data according to the font and the writing system's rendering rules.</summary>
        /// <param name="textString">The string to convert to glyphs.</param>
        /// <param name="textLength">The length of textString.</param>
        /// <param name="fontFace">The font face to get glyphs from.</param>
        /// <param name="isSideways">Set to true if the text is intended to be drawn vertically.</param>
        /// <param name="isRightToLeft">Set to TRUE for right-to-left text.</param>
        /// <param name="scriptAnalysis">Script analysis result from AnalyzeScript.</param>
        /// <param name="localeName">The locale to use when selecting glyphs. e.g. the same character may map to different glyphs for ja-jp vs zh-chs. If this is NULL then the default mapping based on the script is used.</param>
        /// <param name="numberSubstitution">Optional number substitution which selects the appropriate glyphs for digits and related numeric characters, depending on the results obtained from AnalyzeNumberSubstitution. Passing null indicates that no substitution is needed and that the digits should receive nominal glyphs.</param>
        /// <param name="features">An array of pointers to the sets of typographic features to use in each feature range.</param>
        /// <param name="featureRangeLengths">The length of each feature range, in characters. The sum of all lengths should be equal to textLength.</param>
        /// <param name="featureRanges">The number of feature ranges.</param>
        /// <param name="maxGlyphCount">The maximum number of glyphs that can be returned.</param>
        /// <param name="clusterMap">The mapping from character ranges to glyph ranges.</param>
        /// <param name="textProps">Per-character output properties.</param>
        /// <param name="glyphIndices">Output glyph indices.</param>
        /// <param name="glyphProps">Per-glyph output properties.</param>
        /// <param name="actualGlyphCount">The actual number of glyphs returned if the call succeeds.</param>
        /// <returns>Standard HRESULT error code.</returns>
        /// <remarks> Note that the mapping from characters to glyphs is, in general, many- to-many.  The recommended estimate for the per-glyph output buffers is (3 * textLength / 2 + 16).  This is not guaranteed to be sufficient. The value of the actualGlyphCount parameter is only valid if the call succeeds.  In the event that maxGlyphCount is not big enough E_NOT_SUFFICIENT_BUFFER, which is equivalent to HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER), will be returned.  The application should allocate a larger buffer and try again.</remarks>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int GetGlyphs(
            [In] IDWriteTextAnalyzer* This,
            [In, ComAliasName("WCHAR")] /* readonly */ char* textString,
            [In, ComAliasName("UINT32")] uint textLength,
            [In] IDWriteFontFace* fontFace,
            [In, ComAliasName("BOOL")] int isSideways,
            [In, ComAliasName("BOOL")] int isRightToLeft,
            [In] /* readonly */ DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [In, Optional, ComAliasName("WCHAR")] /* readonly */ char* localeName,
            [In, Optional] IDWriteNumberSubstitution* numberSubstitution,
            [In, Optional] /* readonly */ DWRITE_TYPOGRAPHIC_FEATURES** features,
            [In, Optional, ComAliasName("UINT32")] /* readonly */ uint* featureRangeLengths,
            [In, ComAliasName("UINT32")] uint featureRanges,
            [In, ComAliasName("UINT32")] uint maxGlyphCount,
            [Out, ComAliasName("UINT16")] ushort* clusterMap,
            [Out] DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
            [Out, ComAliasName("UINT16")] ushort* glyphIndices,
            [Out] DWRITE_SHAPING_GLYPH_PROPERTIES* glyphProps,
            [Out, ComAliasName("UINT32")] uint* actualGlyphCount
        );

        /// <summary>Place glyphs output from the GetGlyphs method according to the font and the writing system's rendering rules.</summary>
        /// <param name="textString">The original string the glyphs came from.</param>
        /// <param name="clusterMap">The mapping from character ranges to glyph ranges. Returned by GetGlyphs.</param>
        /// <param name="textProps">Per-character properties. Returned by GetGlyphs.</param>
        /// <param name="textLength">The length of textString.</param>
        /// <param name="glyphIndices">Glyph indices. See GetGlyphs</param>
        /// <param name="glyphProps">Per-glyph properties. See GetGlyphs</param>
        /// <param name="glyphCount">The number of glyphs.</param>
        /// <param name="fontFace">The font face the glyphs came from.</param>
        /// <param name="fontEmSize">Logical font size in DIP's.</param>
        /// <param name="isSideways">Set to true if the text is intended to be drawn vertically.</param>
        /// <param name="isRightToLeft">Set to TRUE for right-to-left text.</param>
        /// <param name="scriptAnalysis">Script analysis result from AnalyzeScript.</param>
        /// <param name="localeName">The locale to use when selecting glyphs. e.g. the same character may map to different glyphs for ja-jp vs zh-chs. If this is NULL then the default mapping based on the script is used.</param>
        /// <param name="features">An array of pointers to the sets of typographic features to use in each feature range.</param>
        /// <param name="featureRangeLengths">The length of each feature range, in characters. The sum of all lengths should be equal to textLength.</param>
        /// <param name="featureRanges">The number of feature ranges.</param>
        /// <param name="glyphAdvances">The advance width of each glyph.</param>
        /// <param name="glyphOffsets">The offset of the origin of each glyph.</param>
        /// <returns>Standard HRESULT error code.</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int GetGlyphPlacements(
            [In] IDWriteTextAnalyzer* This,
            [In, ComAliasName("WCHAR")] /* readonly */ char* textString,
            [In, ComAliasName("UINT16")] /* readonly */ ushort* clusterMap,
            [In] DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
            [In, ComAliasName("UINT32")] uint textLength,
            [In, ComAliasName("UINT16")] /* readonly */ ushort* glyphIndices,
            [In] /* readonly */ DWRITE_SHAPING_GLYPH_PROPERTIES* glyphProps,
            [In, ComAliasName("UINT32")] uint glyphCount,
            [In] IDWriteFontFace* fontFace,
            [In, ComAliasName("FLOAT")] float fontEmSize,
            [In, ComAliasName("BOOL")] int isSideways,
            [In, ComAliasName("BOOL")] int isRightToLeft,
            [In] /* readonly */ DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [In, Optional, ComAliasName("WCHAR")] /* readonly */ char* localeName,
            [In, Optional] /* readonly */ DWRITE_TYPOGRAPHIC_FEATURES** features,
            [In, Optional, ComAliasName("UINT32")] /* readonly */ uint* featureRangeLengths,
            [In, ComAliasName("UINT32")] uint featureRanges,
            [Out, ComAliasName("FLOAT")] float* glyphAdvances,
            [Out] DWRITE_GLYPH_OFFSET* glyphOffsets
        );

        /// <summary>Place glyphs output from the GetGlyphs method according to the font and the writing system's rendering rules.</summary>
        /// <param name="textString">The original string the glyphs came from.</param>
        /// <param name="clusterMap">The mapping from character ranges to glyph ranges. Returned by GetGlyphs.</param>
        /// <param name="textProps">Per-character properties. Returned by GetGlyphs.</param>
        /// <param name="textLength">The length of textString.</param>
        /// <param name="glyphIndices">Glyph indices. See GetGlyphs</param>
        /// <param name="glyphProps">Per-glyph properties. See GetGlyphs</param>
        /// <param name="glyphCount">The number of glyphs.</param>
        /// <param name="fontFace">The font face the glyphs came from.</param>
        /// <param name="fontEmSize">Logical font size in DIP's.</param>
        /// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if the DPI of the rendering surface is 96 this value is 1.0f. If the DPI is 120, this value is 120.0f/96.</param>
        /// <param name="transform">Optional transform applied to the glyphs and their positions. This transform is applied after the scaling specified by the font size and pixelsPerDip.</param>
        /// <param name="useGdiNatural"> When set to FALSE, the metrics are the same as the metrics of GDI aliased text. When set to TRUE, the metrics are the same as the metrics of text measured by GDI using a font created with CLEARTYPE_NATURAL_QUALITY.</param>
        /// <param name="isSideways">Set to true if the text is intended to be drawn vertically.</param>
        /// <param name="isRightToLeft">Set to TRUE for right-to-left text.</param>
        /// <param name="scriptAnalysis">Script analysis result from AnalyzeScript.</param>
        /// <param name="localeName">The locale to use when selecting glyphs. e.g. the same character may map to different glyphs for ja-jp vs zh-chs. If this is NULL then the default mapping based on the script is used.</param>
        /// <param name="features">An array of pointers to the sets of typographic features to use in each feature range.</param>
        /// <param name="featureRangeLengths">The length of each feature range, in characters. The sum of all lengths should be equal to textLength.</param>
        /// <param name="featureRanges">The number of feature ranges.</param>
        /// <param name="glyphAdvances">The advance width of each glyph.</param>
        /// <param name="glyphOffsets">The offset of the origin of each glyph.</param>
        /// <returns>Standard HRESULT error code.</returns>
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = false, ThrowOnUnmappableChar = false)]
        [return: ComAliasName("HRESULT")]
        public /* static */ delegate int GetGdiCompatibleGlyphPlacements(
            [In] IDWriteTextAnalyzer* This,
            [In, ComAliasName("WCHAR")] /* readonly */ char* textString,
            [In, ComAliasName("UINT16")] /* readonly */ ushort* clusterMap,
            [In] DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
            [In, ComAliasName("UINT32")] uint textLength,
            [In, ComAliasName("UINT16")] /* readonly */ ushort* glyphIndices,
            [In] /* readonly */ DWRITE_SHAPING_GLYPH_PROPERTIES* glyphProps,
            [In, ComAliasName("UINT32")] uint glyphCount,
            [In] IDWriteFontFace* fontFace,
            [In, ComAliasName("FLOAT")] float fontEmSize,
            [In, ComAliasName("FLOAT")] float pixelsPerDip,
            [In, Optional] /* readonly */ DWRITE_MATRIX* transform,
            [In, ComAliasName("BOOL")] int useGdiNatural,
            [In, ComAliasName("BOOL")] int isSideways,
            [In, ComAliasName("BOOL")] int isRightToLeft,
            [In] /* readonly */ DWRITE_SCRIPT_ANALYSIS* scriptAnalysis,
            [In, Optional, ComAliasName("WCHAR")] /* readonly */ char* localeName,
            [In, Optional] /* readonly */ DWRITE_TYPOGRAPHIC_FEATURES** features,
            [In, Optional, ComAliasName("UINT32")] /* readonly */ uint* featureRangeLengths,
            [In, ComAliasName("UINT32")] uint featureRanges,
            [Out, ComAliasName("FLOAT")] float* glyphAdvances,
            [Out] DWRITE_GLYPH_OFFSET* glyphOffsets
        );
        #endregion

        #region Structs
        public /* blittable */ struct Vtbl
        {
            #region Fields
            public IUnknown.Vtbl BaseVtbl;

            public IntPtr AnalyzeScript;

            public IntPtr AnalyzeBidi;

            public IntPtr AnalyzeNumberSubstitution;

            public IntPtr AnalyzeLineBreakpoints;

            public IntPtr GetGlyphs;

            public IntPtr GetGlyphPlacements;

            public IntPtr GetGdiCompatibleGlyphPlacements;
            #endregion
        }
        #endregion
    }
}
