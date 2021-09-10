using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-inline-script")]
    public partial class SmartInlineScriptTagHelper : TagHelper
    {
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

            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("type", "text/javascript");

            base.Process(context, output);
        }
    }
}
