using System.Web;
using System.Web.Optimization;

namespace HRISapp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/JScripts").Include(
                        "~/Scripts/js/plugins/loaders/pace.min.js",
                        "~/Scripts/js/core/libraries/jquery.min.js",
                        "~/Scripts/js/core/libraries/bootstrap.min.js",
                        "~/Scripts/js/plugins/loaders/blockui.min.js",
                        "~/Scripts/js/core/app.js",
                        "~/Scripts/js/sweetalert/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/Angular/angular.min.js",
                        "~/Scripts/Angular/angular.ng-modules.js",
                        "~/Scripts/Angular/angular-animate.min.js",
                        "~/Scripts/Angular/smart-table.debug.js",
                        "~/Scripts/Angular/angular-touch.min.js",
                        "~/Scripts/Angular/ui-bootstrap-2.5.0.min.js",
                        "~/Scripts/Angular/ui-bootstrap-tpls-2.5.0.min.js",
                        "~/Scripts/Angular/select2.full.min.js",
                        "~/Scripts/Angular/select2.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/css/icons/icomoon/styles.css",
                        "~/Content/css/bootstrap.css",
                        "~/Content/css/core.css",
                        "~/Content/css/components.css",
                        "~/Content/css/colors.css",
                        "~/Content/css/extras/animate.min.css",
                        "~/Content/css/sweetalert/sweetalert.css"));
        }
    }
}
