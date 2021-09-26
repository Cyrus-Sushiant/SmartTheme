using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-editor", Attributes = ForAttributeName)]
    public partial class SmartTextEditorTagHelper : SmartInputTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string SkinAttributeName = "asp-skin";
        private const string CSSClassAttributeName = "class";
        private const string NameAttributeName = "asp-name";
        private const string CSSClassInputAttributeName = "asp-input-class";
        private const string FaIconAttributeName = "asp-fa-icon";
        private const string AutocompleteOffAttributeName = "asp-autocomplete-off";
        private const string VisibleAttributeName = "asp-visible";
        private const string SizeAttributeName = "asp-col-size";
        private const string PlaceHolderAttributeName = "asp-placeholder";
        private const string PlaceHolderTextAttributeName = "asp-placeholder-text";
        private const string ReadonlyAttributeName = "asp-readonly";
        private const string DisplayNameAttributeName = "asp-displayname";

        [HtmlAttributeName(FaIconAttributeName)]
        public string FaIcon { get; set; }

        [HtmlAttributeName(AutocompleteOffAttributeName)]
        public bool AutocompleteOff { get; set; }

        [HtmlAttributeName(VisibleAttributeName)]
        public bool Visible { get; set; } = true;

        [HtmlAttributeName(SizeAttributeName)]
        public int ColSize { get; set; } = 4;

        [HtmlAttributeName(PlaceHolderAttributeName)]
        public bool Placeholder { get; set; } = true;

        [HtmlAttributeName(PlaceHolderTextAttributeName)]
        public string PlaceholderText { get; set; }

        [HtmlAttributeName(DisplayNameAttributeName)]
        public string DisplayName { get; set; }

        [HtmlAttributeName(CSSClassAttributeName)]
        public string CSSClass { get; set; }

        [HtmlAttributeName(NameAttributeName)]
        public string Name { get; set; }

        [HtmlAttributeName(CSSClassInputAttributeName)]
        public string InputCSSClass { get; set; }

        [HtmlAttributeName(ReadonlyAttributeName)]
        public bool Readonly { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="generator">HTML generator</param>
        public SmartTextEditorTagHelper(IHtmlGenerator generator) : base(generator)
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

            string inputCSSClass = string.Empty;
            if (Skin == SmartEditorSkins.Default)
            {
                inputCSSClass = "form-control";
            }

            if (!string.IsNullOrEmpty(InputCSSClass))
            {
                inputCSSClass = $"{inputCSSClass} {InputCSSClass}";
            }

            if (!string.IsNullOrEmpty(inputCSSClass))
            {
                output.Attributes.Add("class", inputCSSClass);
            }

            if (AutocompleteOff)
            {
                output.Attributes.Add("autocomplete", "off");
            }

            if (Readonly)
            {
                output.Attributes.Add("readonly", true);
            }

            output.SuppressOutput();

            output.TagName = "input";

            if (Placeholder)
            {
                if (string.IsNullOrEmpty(PlaceholderText))
                {
                    output.Attributes.Add("placeholder", For.Metadata.DisplayName);
                }
                else
                {
                    output.Attributes.Add("placeholder", PlaceholderText);
                }
            }

            if (string.IsNullOrEmpty(DisplayName))
            {
                DisplayName = For.Metadata.DisplayName;
            }

            string colSize = SmartMainHelper.GetColSize(ColSize);

            if (!string.IsNullOrEmpty(CSSClass))
            {
                colSize = $"{colSize} {CSSClass}";
            }

            string name = For.Name;
            if (!string.IsNullOrEmpty(Name))
            {
                name = Name;
            }

            if (string.IsNullOrEmpty(FaIcon))
            {
                output.PreElement.SetHtmlContent($@"<div class='{colSize} form-group'>
                        <label >
                            {DisplayName}
                        </label>");
                output.PostElement.SetHtmlContent($@"<label lass='validation-error-label' data-valmsg-replace='true' data-valmsg-for='{name}'></label>
                    </div>");
            }
            else
            {
                output.PreElement.SetHtmlContent($@"<div class='{colSize} form-group'>
                        <label>
                            {DisplayName}
                        </label><div class='input-group'>");
                output.PostElement.SetHtmlContent($@"  <span class='input-group-addon'><i class='fa fa-{(string.IsNullOrEmpty(FaIcon) ? "angle-double-left" : FaIcon)}'></i></span></div>
                       <small class='validation-error-label' data-valmsg-replace='true' data-valmsg-for='{name}'></small>
                    </div>");
            }


            base.Process(context, output);
        }
    }
}
