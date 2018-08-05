using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace AbitYour.Views.Shared.Components
{
    public class MobileBannerViewComponent : ViewComponent
    {
        private static readonly string MobileBanner;

        static MobileBannerViewComponent()
        {
            MobileBanner =
                "<script type=\"text/javascript\">\r\n  ( function() {\r\n    if (window.CHITIKA === undefined) { window.CHITIKA = { \'units\' : [] }; };\r\n    var unit = {\"calltype\":\"async[2]\",\"publisher\":\"SaveliyK\",\"width\":320,\"height\":50,\"sid\":\"Chitika Default\"};\r\n    var placement_id = window.CHITIKA.units.length;\r\n    window.CHITIKA.units.push(unit);\r\n    document.write(\'<div id=\"chitikaAdBlock-\' + placement_id + \'\"></div>\');\r\n}());\r\n</script>\r\n<script type=\"text/javascript\" src=\"//cdn.chitika.net/getads.js\" async></script>";
        }

        public IViewComponentResult Invoke()
        {
            return new HtmlContentViewComponentResult(new HtmlString(MobileBanner));
        }
    }
}
