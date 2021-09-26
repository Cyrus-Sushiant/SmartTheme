namespace SmartTheme.Tags
{
    public static class SmartMainHelper
    {
        public static string GetColSize(int ColSize)
        {
            switch (ColSize)
            {
                case 2:
                    return "col-lg-2 col-md-2 col-sm-6 col-xs-12";
                case 3:
                    return "col-lg-3 col-md-3 col-sm-6 col-xs-12";
                case 4:
                    return "col-lg-4 col-md-4 col-sm-6 col-xs-12";
                case 5:
                    return "col-lg-5 col-md-5 col-sm-5 col-xs-12";
                case 6:
                    return "col-lg-6 col-md-6 col-sm-6 col-xs-12";
                case 7:
                    return "col-lg-7 col-md-7 col-sm-7 col-xs-12";
                case 8:
                    return "col-lg-8 col-md-8 col-sm-8 col-xs-12";
                case 9:
                    return "col-lg-9 col-md-9 col-sm-12 col-xs-12";
                case 10:
                    return "col-lg-10 col-md-10 col-sm-12 col-xs-12";
                case 11:
                    return "col-lg-11 col-md-11 col-sm-12 col-xs-12";
                default:
                    return "col-lg-12 col-md-12 col-sm-12 col-xs-12";
            }
        }
    }
}
