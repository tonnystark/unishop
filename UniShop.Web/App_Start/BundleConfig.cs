using System.Web.Optimization;

namespace UniShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/Assets/client/js/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/js/core")
                .Include("~/Assets/admin/libs/jquery-ui/jquery-ui.min.js",
                    "~/Assets/admin/libs/jquery-validation/dist/jquery.validate.js",
                    "~/Assets/admin/libs/mustache/mustache.js",
                    "~/Assets/admin/libs/numeral/numeral.js",
                    "~/Assets/client/js/common.js"
                ));

            bundles.Add(new StyleBundle("~/css/base")
                .Include("~/Assets/client/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/custom.css", new CssRewriteUrlTransform())
                .Include("~/Assets/admin/libs/jquery-ui/themes/smoothness/jquery-ui.min.css",
                    new CssRewriteUrlTransform())
                .Include("~/Assets/client/font-awesome-4.7.0/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            BundleTable.EnableOptimizations = true;
        }
    }
}