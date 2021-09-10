using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-row")]
    public partial class SmartRowTagHelper : TagHelper
    {
        private const string VisibleAttributeName = "asp-visible";
        private const string CSSClassAttributeName = "class";

        [HtmlAttributeName(VisibleAttributeName)]
        public bool Visible { get; set; } = true;

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

            string css = "section row";
            if (!string.IsNullOrEmpty(CSSClass))
            {
                css = $"{css} {CSSClass}";
            }

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", css);

            base.Process(context, output);
        }
    }
}
