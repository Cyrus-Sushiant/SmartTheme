using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-select", Attributes = ForAttributeName)]
    public partial class SmartSelectTagHelper : SelectTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string SizeAttributeName = "asp-col-size";
        private const string VisibleAttributeName = "asp-visible";
        private const string DisabledAttributeName = "asp-disabled";
        private const string IDAttributeName = "asp-id";
        private const string MultipleAttributeName = "asp-multiple";
        

        [HtmlAttributeName(SizeAttributeName)]
        public int ColSize { get; set; } = 4;

        [HtmlAttributeName(VisibleAttributeName)]
        public bool Visible { get; set; } = true;

        [HtmlAttributeName(MultipleAttributeName)]
        public bool Multiple { get; set; }

        [HtmlAttributeName(DisabledAttributeName)]
        public bool Disabled { get; set; }
        [HtmlAttributeName(IDAttributeName)]
        public string ID { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        public SmartSelectTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

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

            if (!string.IsNullOrEmpty(ID))
            {
                output.Attributes.Add("ID", ID);
            }
            output.Attributes.Add("class", "form-control");

            output.TagName = "select";
            output.TagMode = TagMode.StartTagAndEndTag;

            string colSize = SmartMainHelper.GetColSize(ColSize);

            output.PreElement.AppendHtml($@"<div class='{colSize} form-group'>
                        <label>
                            {For.Metadata.DisplayName}
                        </label>
                       ");

            output.PostElement.AppendHtml($@"
                     <small class='validation-error-label' data-valmsg-replace='true' data-valmsg-for='{For.Name}'></small>
                    </div>");

            if (Multiple)
            {
                output.Attributes.Add("multiple", "multiple");
            }

            if (Disabled)
            {
                output.Attributes.Add("disabled", true);
                output.PreElement.AppendHtml($"<input type='hidden' name='{For.Name}' value='{For.Model.ToString()}' />");
            }

            base.Process(context, output);
        }
    }
}
