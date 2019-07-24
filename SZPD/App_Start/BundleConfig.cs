using System.Web;
using System.Web.Optimization;

namespace SZPD
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/vue").Include(
                        "~/node_modules/vue/dist/vue.js"));

            bundles.Add(new ScriptBundle("~/bundles/vue_min").Include(
                        "~/Scripts/vue.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jq").Include(
                        "~/node_modules/jquery/dist/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalidate").Include(
                        "~/node_modules/jquery-validation/dist/jquery.validate.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/Content/myCss").Include(
                      "~/Content/myCss.css"));
        }
    }
}
