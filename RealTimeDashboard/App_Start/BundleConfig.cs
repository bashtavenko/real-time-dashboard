using System.Web.Optimization;

namespace RealTimeDashboard
{
    public class BundleConfig
    {
        // TODO: Grunt/gulp
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/angular").Include(
                "~/lib/angular/angular.js",
                "~/lib/angular-resource/angular-resource.js"));

            bundles.Add(new ScriptBundle("~/wijmo").Include(
             "~/wijmo/wijmo.min.js",
             "~/wijmo/wijmo.chart.min.js",
             "~/wijmo/wijmo.grid.min.js",
             "~/wijmo/wijmo.angular.min.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/lib/bootswatch-dist/css/bootstrap.css",
                      "~/css/site.css"));
        }
    }
}
