using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-js-select2")]
    public partial class SmartSelect2JsTagHelper : SmartInlineScriptTagHelper
    {
        private const string ForListAttributeName = "asp-for-list";
        private const string ForAttributeName = "asp-for";
        private const string ForAllAttributeName = "asp-for-all";
        private const string AddResourceAttributeName = "asp-add-resource";
        private const string MinimumResultsForSearchAttributeName = "asp-min-search";
        private const string ActiveTageModeAttributeName = "asp-tags";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(MinimumResultsForSearchAttributeName)]
        public bool MinimumResultsForSearch { get; set; }

        [HtmlAttributeName(AddResourceAttributeName)]
        public bool AddResource { get; set; }

        [HtmlAttributeName(ForListAttributeName)]
        public Dictionary<string, bool> ForList { get; set; }

        [HtmlAttributeName(ForAllAttributeName)]
        public bool ForAll { get; set; }

        [HtmlAttributeName(ActiveTageModeAttributeName)]
        public bool ActiveTageMode { get; set; }


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
                output.PreElement.AppendHtml("<script src='/themes/default/js/plugins/forms/selects/select2.min.js'></script>");
            }

            string js = null;
            if (ForAll)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("$(document).ready(function () {");
                if (MinimumResultsForSearch)
                {
                    sb.Append("$('select').select2();");
                }
                else
                {
                    sb.Append("$('select').select2({ minimumResultsForSearch: -1 });");
                }
                sb.AppendLine("});");

                js = sb.ToString();
            }
            else if (ForList == null)
            {
                if (MinimumResultsForSearch)
                {
                    js = $@"$(document).ready(function () {{
                        $('#{For.Name}').select2();
                    }});";
                }
                else
                {
                    if (ActiveTageMode)
                    {
                        js = $@"$(document).ready(function () {{
                                $('#{For.Name}').select2({{ minimumResultsForSearch: -1, tags: true }});
                                }});";
                    }
                    else
                    {
                        js = $@"$(document).ready(function () {{
                            $('#{For.Name}').select2({{ minimumResultsForSearch: -1 }});
                        }});";
                    }

                }
            }
            else if (ForList?.Any() ?? false)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("$(document).ready(function () {");
                foreach (var item in ForList)
                {
                    if (item.Value)
                    {
                        sb.Append("$('#").Append(item.Key).Append("').select2();");
                    }
                    else
                    {
                        sb.Append("$('#").Append(item.Key).Append("').select2({ minimumResultsForSearch: -1 });");
                    }
                }
                sb.AppendLine("});");

                js = sb.ToString();
            }
            output.PostContent.AppendHtml(js.Replace("  ", string.Empty));
            base.Process(context, output);
        }
    }
}
