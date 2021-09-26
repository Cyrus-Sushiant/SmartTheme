using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using SmartTheme.Core.Models.Alert;
using SmartTheme.Core.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace SmartTheme.Tags
{
    [HtmlTargetElement("smart-alert")]
    public partial class SmartAlertTagHelper : TagHelper
    {
        private const string VisibleAttributeName = "asp-visible";
        private const string TitleAttributeName = "asp-title";
        private const string TextAttributeName = "asp-text";
        private const string ListAttributeName = "asp-list";
        private const string ShowBigSizeAttributeName = "asp-show-big-size";
        private const string ButtonTextAttributeName = "asp-button-text";
        private const string CloseButtonAttributeName = "asp-close-button";
        private const string AlertTypeAttributeName = "asp-type";
        private const string FaIconAttributeName = "asp-fa-icon";
        private const string ButtonJsOnClickAttributeName = "asp-onclick";
        private const string ButtonUrlAttributeName = "asp-url";
        private const string ViewDataAttributeName = "asp-view-data";
        private const string BindViewDataAttributeName = "asp-bind-view-data";
        private const string SetBgAttributeName = "asp-set-bg";
        private const string BtnNameAttributeName = "asp-btn-name";
        private const string BtnValueAttributeName = "asp-btn-value";

        private const string ActionAttributeName = "asp-action";
        private const string ControllerAttributeName = "asp-controller";
        private const string AreaAttributeName = "asp-area";
        private const string FragmentAttributeName = "asp-fragment";
        private const string HostAttributeName = "asp-host";
        private const string ProtocolAttributeName = "asp-protocol";
        private const string RouteAttributeName = "asp-route";
        private const string RouteValuesDictionaryName = "asp-all-route-data";
        private const string RouteValuesPrefix = "asp-route-";

        private IDictionary<string, string> _routeValues;
        private readonly IIdentityService _identityService;

        [HtmlAttributeName(VisibleAttributeName)]
        public bool Visible { get; set; } = true;

        [HtmlAttributeName(TitleAttributeName)]
        public string Title { get; set; }

        [HtmlAttributeName(TextAttributeName)]
        public string Text { get; set; }

        [HtmlAttributeName(ListAttributeName)]
        public string[] List { get; set; }

        [HtmlAttributeName(ShowBigSizeAttributeName)]
        public bool ShowBigSize { get; set; }

        [HtmlAttributeName(CloseButtonAttributeName)]
        public bool CloseButton { get; set; }

        [HtmlAttributeName(ButtonTextAttributeName)]
        public string ButtonText { get; set; }

        [HtmlAttributeName(ButtonJsOnClickAttributeName)]
        public string ButtonJsOnClick { get; set; }

        [HtmlAttributeName(ButtonUrlAttributeName)]
        public string ButtonUrl { get; set; }

        [HtmlAttributeName(AlertTypeAttributeName)]
        public AlertType AlertType { get; set; }

        [HtmlAttributeName(FaIconAttributeName)]
        public string FaIcon { get; set; }

        [HtmlAttributeName(ViewDataAttributeName)]
        public dynamic ViewData { get; set; }

        [HtmlAttributeName(BindViewDataAttributeName)]
        public bool BindViewData { get; set; }

        [HtmlAttributeName(SetBgAttributeName)]
        public bool SetBg { get; set; }

        [HtmlAttributeName(BtnNameAttributeName)]
        public string ButtonName { get; set; }

        [HtmlAttributeName(BtnValueAttributeName)]
        public string ButtonValue { get; set; }



        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }
        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }
        [HtmlAttributeName(AreaAttributeName)]
        public string Area { get; set; }
        [HtmlAttributeName(ProtocolAttributeName)]
        public string Protocol { get; set; }
        [HtmlAttributeName(HostAttributeName)]
        public string Host { get; set; }
        [HtmlAttributeName(FragmentAttributeName)]
        public string Fragment { get; set; }
        [HtmlAttributeName(RouteAttributeName)]
        public string Route { get; set; }
        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (_routeValues == null)
                {
                    _routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return _routeValues;
            }
            set
            {
                _routeValues = value;
            }
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        protected IHtmlGenerator Generator { get; }

        public SmartAlertTagHelper(IHtmlGenerator generator, IIdentityService identityService)
        {
            Generator = generator;
            _identityService = identityService;
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

            if (BindViewData && ViewData != null)
            {
                var viewDataModel = (AlertModel)ViewData;
                Visible = true;
                Title = viewDataModel.Title;
                Text = viewDataModel.Text;
                List = viewDataModel.List;
                ShowBigSize = viewDataModel.ShowBigSize;
                CloseButton = viewDataModel.CloseButton.GetValueOrDefault(true);
                ButtonText = viewDataModel.ButtonText;
                ButtonJsOnClick = viewDataModel.ButtonJsOnClick;
                ButtonUrl = viewDataModel.ButtonUrl;
                AlertType = (AlertType)Enum.ToObject(typeof(AlertType), (int)viewDataModel.Type);
                FaIcon = viewDataModel.IconCssClass;
            }
            else if (BindViewData && ViewData == null)
            {
                Visible = false;
            }

            if (!Visible || !(!string.IsNullOrEmpty(Text) || (List?.Any() ?? false)))
            {
                output.TagName = "";
                output.Content.SetHtmlContent("");
                return;
            }

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            string closeButton = null;
            if (CloseButton)
            {
                closeButton = "<button type='button' class='close' data-dismiss='alert'><span>&times;</span><span class='sr-only'>Close</span></button>";
            }

            StringBuilder messageColorCssClass = new StringBuilder();


            if (SetBg)
            {
                switch (AlertType)
                {
                    case AlertType.Success:
                        messageColorCssClass.Append("bg-success");
                        break;
                    case AlertType.Danger:
                        messageColorCssClass.Append("bg-danger");
                        break;
                    case AlertType.Warning:
                        messageColorCssClass.Append("bg-warning");
                        break;
                    case AlertType.Info:
                        messageColorCssClass.Append("bg-info");
                        break;
                    default:
                        messageColorCssClass.Append("bg-info");
                        break;
                }
            }
            else
            {
                messageColorCssClass.Append(" ");
                switch (AlertType)
                {
                    case AlertType.Success:
                        messageColorCssClass.Append("alert-success");
                        break;
                    case AlertType.Danger:
                        messageColorCssClass.Append("alert-danger");
                        break;
                    case AlertType.Warning:
                        messageColorCssClass.Append("alert-warning");
                        break;
                    case AlertType.Info:
                        messageColorCssClass.Append("alert-info");
                        break;
                    default:
                        messageColorCssClass.Append("alert-info");
                        break;
                }

                messageColorCssClass.Append(" alert-dismissable mb30 pr15");
            }

            string messageIconCssClass = FaIcon;
            if (string.IsNullOrEmpty(messageIconCssClass))
            {
                switch (AlertType)
                {
                    case AlertType.Success:
                        messageIconCssClass = "fa-check-circle";
                        break;
                    case AlertType.Danger:
                        messageIconCssClass = "fa-times-circle";
                        break;
                    case AlertType.Warning:
                        messageIconCssClass = "fa-warning";
                        break;
                    case AlertType.Info:
                        messageIconCssClass = "fa-info-circle";
                        break;
                    default:
                        messageIconCssClass = "fa-info-circle";
                        break;
                }
            }

            if (ShowBigSize || !string.IsNullOrEmpty(ButtonText) || (List?.Any() ?? false))
            {
                var textBody = new StringBuilder();
                if (List?.Any() ?? false)
                {
                    textBody.Append("<p><ul>").Append(Text);
                    foreach (var item in List)
                    {
                        textBody.Append("<li>").Append(item).Append("</li>");
                    }
                    textBody.Append("</ul></p>");
                }
                else
                {
                    textBody.Append("<p>").Append(Text).Append("</p>");
                }

                var btnBody = new StringBuilder();
                if (!string.IsNullOrEmpty(ButtonText) && this.CheckButtonAccess())
                {
                    btnBody.Append("<br />")
                        .Append("<p class='text-right'>");
                    if (!string.IsNullOrEmpty(ButtonJsOnClick))
                    {
                        btnBody.Append("<span class='btn btn-default cursor-pointer' onclick='").Append(ButtonJsOnClick).Append("'>")
                            .Append(ButtonText).Append("</span>");
                    }
                    else if (!string.IsNullOrEmpty(ButtonName))
                    {
                        btnBody.Append("<button class='btn btn-default' type='submit' name='").Append(ButtonName).Append("' ")
                           .Append("value='").Append(ButtonValue).Append("'>")
                           .Append(ButtonText)
                           .Append("</button>");
                    }
                    else if (string.IsNullOrEmpty(ButtonUrl))
                    {
                        var btnHtml = this.GetButtonUrl();
                        if (!string.IsNullOrEmpty(btnHtml))
                        {
                            btnBody.Append(btnHtml);
                        }
                    }
                    else if (ButtonUrl.StartsWith("#"))
                    {
                        btnBody.Append("<a class='btn btn-default' data-toggle='modal' data-target='").Append(ButtonUrl).Append("'>")
                            .Append(ButtonText)
                            .Append("</a>");
                    }
                    else
                    {
                        btnBody.Append("<a class='btn btn-default' href='").Append(ButtonUrl).Append("'>")
                                .Append(ButtonText)
                                .Append("</a>");
                    }
                    btnBody.Append("</p>");
                }

                output.Attributes.Add("class", $"alert {messageColorCssClass.ToString()} alert-styled-left");
                output.Content.SetHtmlContent($@"{closeButton}
                                                    <p><strong>
                                                        {(string.IsNullOrEmpty(Title) ? "توجه!" : Title)}
                                                    </strong></p>
                                                    {textBody.ToString()}
                                                    {btnBody.ToString()}");
            }
            else
            {
                output.Attributes.Add("class", $"alert {messageColorCssClass.ToString()} alert-styled-left");
                output.Content.SetHtmlContent($@"{closeButton} {Text}");
            }

            base.Process(context, output);
        }

        private bool CheckButtonAccess()
        {
            if (!string.IsNullOrEmpty(Controller) || !string.IsNullOrEmpty(Action))
            {
                var controller = Controller ?? ViewContext?.RouteData?.Values["controller"].ToString();
                var action = Action ?? ViewContext?.RouteData?.Values["action"].ToString();

                return _identityService.HasPermission(action, controller, Area);
            }

            return true;
        }

        private string GetButtonUrl()
        {
            var routeLink = Route != null;
            var actionLink = Controller != null || Action != null;

            if (!routeLink && !actionLink)
            {
                return null;
            }

            RouteValueDictionary routeValues = null;
            if (_routeValues != null && _routeValues.Count > 0)
            {
                routeValues = new RouteValueDictionary(_routeValues);
            }

            if (Area != null)
            {
                // Unconditionally replace any value from asp-route-area.
                if (routeValues == null)
                {
                    routeValues = new RouteValueDictionary();
                }
                routeValues["area"] = Area;
            }


            TagBuilder tagBuilder;
            if (routeLink)
            {
                tagBuilder = Generator.GenerateRouteLink(
                    ViewContext,
                    linkText: ButtonText,
                    routeName: Route,
                    protocol: Protocol,
                    hostName: Host,
                    fragment: Fragment,
                    routeValues: routeValues,
                    htmlAttributes: new Dictionary<string, object> { { "class", "btn bg-slate" } });
            }
            else
            {
                tagBuilder = Generator.GenerateActionLink(
                   ViewContext,
                   linkText: ButtonText,
                   actionName: Action,
                   controllerName: Controller,
                   protocol: Protocol,
                   hostname: Host,
                   fragment: Fragment,
                   routeValues: routeValues,
                   htmlAttributes: new Dictionary<string, object> { { "class", "btn bg-slate" } });
            }

            if (tagBuilder is null)
            {
                return null;
            }

            //return tagBuilder?.InnerHtml.ToString();

            using (var writer = new System.IO.StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
