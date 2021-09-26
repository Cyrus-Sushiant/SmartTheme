using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartTheme.Core.Services.Identity;
using System;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-link")]
    public partial class SmartLink : AnchorTagHelper
    {
        private const string OnClickAttributeName = "onclick";
        private readonly IIdentityService _identityService;

        [HtmlAttributeName(OnClickAttributeName)]
        public string OnClick { get; set; }

        public SmartLink(IHtmlGenerator generator, IIdentityService identityService) : base(generator)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            this.CustomProcess(context, output);
        }

        private void CustomProcess(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            if (output.TagMode == TagMode.SelfClosing)
            {
                output.TagMode = TagMode.StartTagAndEndTag;
            }

            var controller = Controller ?? ViewContext?.RouteData?.Values["controller"].ToString();
            var action = Action ?? ViewContext?.RouteData?.Values["action"].ToString();

            if (!_identityService.HasPermission(action, controller, Area))
            {
                output.TagName = "";
                output.Content.SetHtmlContent("");
                return;
            }

            if (string.IsNullOrEmpty(OnClick))
            {
                output.TagName = "a";
                base.Process(context, output);
            }
            else
            {
                output.TagName = "span";
                output.Attributes.Add("onclick", OnClick);
            }
        }
    }
}
