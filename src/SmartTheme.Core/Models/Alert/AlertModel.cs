namespace SmartTheme.Core.Models.Alert
{
    public partial class AlertModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string[] List { get; set; }
        public AlertType Type { get; set; }

        public string IconCssClass { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
        public string ButtonJsOnClick { get; set; }
        public bool? CloseButton { get; set; }

        public bool ShowBigSize { get; set; }
    }
}
