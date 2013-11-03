using System.Web;
using System.Web.Optimization;

namespace Wayfarer.Mvc
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/wayfarer").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.signalR-1.0.1.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/modernizr-*",
                        "~/Scripts/knockout-2.2.0.js",
                        "~/Scripts/handlebars.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/wayfarer").Include(
                "~/Content/bootstrap/bootstrap-glyphicons.css",
                "~/Content/bootstrap/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/site.css",
                "~/Content/toastr.css"
                ));

        //    bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
        //                "~/Content/themes/base/jquery.ui.core.css",
        //                "~/Content/themes/base/jquery.ui.resizable.css",
        //                "~/Content/themes/base/jquery.ui.selectable.css",
        //                "~/Content/themes/base/jquery.ui.accordion.css",
        //                "~/Content/themes/base/jquery.ui.autocomplete.css",
        //                "~/Content/themes/base/jquery.ui.button.css",
        //                "~/Content/themes/base/jquery.ui.dialog.css",
        //                "~/Content/themes/base/jquery.ui.slider.css",
        //                "~/Content/themes/base/jquery.ui.tabs.css",
        //                "~/Content/themes/base/jquery.ui.datepicker.css",
        //                "~/Content/themes/base/jquery.ui.progressbar.css",
        //                "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}