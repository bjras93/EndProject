using System.Web.Optimization;

namespace LifeStruct
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                "~/Content/JS/plugins/jquery-3.1.0.min.js",
                "~/Content/JS/plugins/jquery-ui.min.js"
                ));
        }
    }
}
