using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-textarea", Attributes = ForAttributeName)]
    public partial class SmartEditorTagHelper : TextAreaTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string DisplayNameAttributeName = "asp-displayname";
        private const string SizeAttributeName = "asp-col-size";
        private const string CSSClassAttributeName = "class";

        /// <summary>
        /// An expression to be evaluated against the current model
        /// </summary>
        [HtmlAttributeName(DisplayNameAttributeName)]
        public string DisplayName { get; set; }

        [HtmlAttributeName(CSSClassAttributeName)]
        public string CSSClass { get; set; }

        [HtmlAttributeName(SizeAttributeName)]
        public int ColSize { get; set; } = 4;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        public SmartEditorTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string colSize = SmartMainHelper.GetColSize(ColSize);

            if (!string.IsNullOrEmpty(CSSClass))
            {
                colSize = $"{colSize} {CSSClass}";
            }
            output.SuppressOutput();
            output.TagName = "textarea";
            output.TagMode = TagMode.StartTagAndEndTag;

            if (string.IsNullOrEmpty(DisplayName))
            {
                DisplayName = For.Metadata.DisplayName;
            }

            output.Attributes.Add(new TagHelperAttribute("class", "wysihtml5 wysihtml5-min form-control"));

            output.PreElement.SetHtmlContent($@"<div class='{colSize} form-group'>
                        <label>
                            {DisplayName}
                        </label><div class='input-group'>");
            output.PostElement.SetHtmlContent($@"</div>
                       <small class='validation-error-label' data-valmsg-replace='true' data-valmsg-for='{For.Name}'></small>
                    </div>");

            base.Process(context, output);
        }
    }
}
