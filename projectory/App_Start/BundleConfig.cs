using System.Web.Optimization;

namespace projectory
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.js",
                "~/Scripts/jqBootstrapValidation.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                "~/Content/bootstrap.css",
                "~/Content/admin.css",
                "~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                "~/Scripts/tinymce/tinymce.js",
                "~/Scripts/tinymceInit.js"));
        }
    }
}
