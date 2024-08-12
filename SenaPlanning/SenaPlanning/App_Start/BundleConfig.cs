using System.Web;
using System.Web.Optimization;

namespace SenaPlanning
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new Bundle("~/bundles/assets").Include(
            "~/assets/js/oneui.app.min.js",
            "~/assets/js/plugins/chart.js/chart.min.js",
            "~/assets/js/pages/be_pages_dashboard.min.js",
            "~/assets/js/lib/jquery.min.js",
            "~/assets/js/plugins/datatables/jquery.dataTables.min.js",
            "~/assets/js/plugins/datatables-bs5/js/dataTables.bootstrap5.min.js",
            "~/assets/js/plugins/datatables-responsive/js/dataTables.responsive.min.js",
            "~/assets/js/plugins/datatables-responsive-bs5/js/responsive.bootstrap5.min.js",
            "~/assets/js/plugins/datatables-buttons/dataTables.buttons.min.js",
            "~/assets/js/plugins/datatables-buttons-bs5/js/buttons.bootstrap5.min.js",
            "~/assets/js/plugins/datatables-buttons-jszip/jszip.min.js",
            "~/assets/js/plugins/datatables-buttons-pdfmake/pdfmake.min.js",
            "~/assets/js/plugins/datatables-buttons-pdfmake/vfs_fonts.js",
            "~/assets/js/plugins/datatables-buttons/buttons.print.min.js",
            "~/assets/js/plugins/datatables-buttons/buttons.html5.min.js",
            "~/assets/js/pages/be_tables_datatables.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/assets").Include(
                      "~/assets/js/plugins/datatables-bs5/css/dataTables.bootstrap5.min.css",
                      "~/assets/js/plugins/datatables-buttons-bs5/css/buttons.bootstrap5.min.css",
                      "~/assets/js/plugins/datatables-responsive-bs5/css/responsive.bootstrap5.min.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
