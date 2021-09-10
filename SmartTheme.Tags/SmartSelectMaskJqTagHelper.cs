using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-mask-jq")]
    public partial class SmartSelectMaskJqTagHelper : SmartInlineScriptTagHelper
    {
        private const string ForListAttributeName = "asp-for-list";
        private const string ForAttributeName = "asp-for";
        private const string ClassAttributeName = "asp-for-class";
        private const string MaskAttributeName = "asp-mask";
        private const string AddResourceAttributeName = "asp-add-resource";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(AddResourceAttributeName)]
        public bool AddResource { get; set; }

        [HtmlAttributeName(ForListAttributeName)]
        public Dictionary<string, string> ForList { get; set; }

        [HtmlAttributeName(MaskAttributeName)]
        public string Mask { get; set; }

        [HtmlAttributeName(ClassAttributeName)]
        public string ForClass { get; set; }

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

            if (AddResource)
            {
                output.PreElement.AppendHtml("<script src='/lib/jquery-mask-plugin/src/jquery.mask.js'></script>");
            }

            string js = null;
            if (ForList == null)
            {
                if (string.IsNullOrEmpty(ForClass))
                {
                    js = $@"$(document).ready(function () {{
                        $('#{For.Name}').mask('{Mask}');
                    }});";
                }
                else
                {
                    js = $@"$(document).ready(function () {{
                        $('.{ForClass}').mask('{Mask}');
                    }});";
                }
            }
            else if (ForList?.Any() ?? false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("$(document).ready(function () {");
                foreach (var item in ForList)
                {
                    sb.Append("$('#").Append(item.Key).Append($"').mask('").Append(item.Value).Append("');");
                }
                sb.AppendLine("});");

                js = sb.ToString();
            }
            output.PostContent.AppendHtml(js.Replace("  ", string.Empty));
            base.Process(context, output);
        }
    }
}
