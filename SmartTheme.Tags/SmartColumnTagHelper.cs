using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-column")]
    public partial class SmartColumnTagHelper : TagHelper
    {
        private const string VisibleAttributeName = "asp-visible";
        private const string SizeAttributeName = "asp-col-size";
        private const string CSSClassAttributeName = "class";

        [HtmlAttributeName(VisibleAttributeName)]
        public bool Visible { get; set; } = true;

        [HtmlAttributeName(SizeAttributeName)]
        public int ColSize { get; set; } = 4;

        [HtmlAttributeName(CSSClassAttributeName)]
        public string CSSClass { get; set; }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (!Visible)
            {
                output.TagName = "";
                output.Content.SetHtmlContent("");
                return;
            }

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            string colSize = SmartMainHelper.GetColSize(ColSize);
            if (!string.IsNullOrEmpty(CSSClass))
            {
                colSize = $"{colSize} {CSSClass}";
            }

            output.Attributes.Add("class", colSize);

            base.Process(context, output);
        }
    }
}
