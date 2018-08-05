using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace AbitYour.Components
{
    public class WideSkyscraperViewComponent : ViewComponent
    {
        private static readonly string WideSkyscrapperBanner;

        static WideSkyscraperViewComponent()
        {
            WideSkyscrapperBanner = @"<script type=""text/javascript"">
  ( function() {
    if (window.CHITIKA === undefined) { window.CHITIKA = { 'units' : [] }; };
    var unit = {""calltype"":""async[2]"",""publisher"":""SaveliyK"",""width"":160,""height"":600,""sid"":""Chitika Default""};
    var placement_id = window.CHITIKA.units.length;
    window.CHITIKA.units.push(unit);
    document.write('<div id=""chitikaAdBlock-' + placement_id + '""></div>');
}());
</script>
<script type=""text/javascript"" src=""//cdn.chitika.net/getads.js"" async></script>";
        }

        public IViewComponentResult Invoke()
        {
            return new HtmlContentViewComponentResult(new HtmlString(WideSkyscrapperBanner));

        }
    }
}
