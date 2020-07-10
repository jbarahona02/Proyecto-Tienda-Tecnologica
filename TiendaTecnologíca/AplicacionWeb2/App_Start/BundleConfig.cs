using System.Web;
using System.Web.Optimization;

namespace AplicacionWeb2
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Importa el jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                          "~/assets/jquery/jquery.min.js"));

            // Importa el js del semantic
            bundles.Add(new ScriptBundle("~/bundles/SemanticJS").Include(
                          "~/assets/Semantic UI/semantic.min.js"));

            // Importar el js de iziToast
            bundles.Add(new ScriptBundle("~/bundles/iziToastJS").Include(
                           "~/assets/js/iziToast.min.js"));

            // Importa mi js perzonalizado
            bundles.Add(new ScriptBundle("~/bundles/Loginjs").Include(
                            "~/assets/js/Login.js"));

            // Importar el css del semantic
            bundles.Add(new StyleBundle("~/bundles/SemanticCSS").Include(
                          "~/assets/Semantic UI/semantic.min.css"));

            // Importa el css de iziToast
            bundles.Add(new StyleBundle("~/bundles/iziToastCSS").Include(
                           "~/assets/css/iziToast.min.css"));

            // Importa mi css perzonalizado
            bundles.Add(new StyleBundle("~/bundles/estiloCSS").Include(
                          "~/assets/css/estilo.css"));

            // Importar el js de principal
            bundles.Add(new ScriptBundle("~/bundles/principalJS").Include(
                            "~/assets/js/Principal.js"));

            // Importar el css de Responsiveslides
            bundles.Add(new StyleBundle("~/bundles/responsiveSlidesCSS").Include(
                          "~/assets/Responsiveslides/responsiveslides.css"));

            // Importa el js de Responsiveslides
            bundles.Add(new ScriptBundle("~/bundles/responsiveSlidesJS").Include(
                            "~/assets/Responsiveslides/responsiveslides.js"));

            // Importa el js del Cliente
            bundles.Add(new ScriptBundle("~/bundles/clienteJS").Include(
                          "~/assets/js/Cliente.js"));

            // Importa el js del Plugin UI de Jquery
            bundles.Add(new ScriptBundle("~/bundles/jqueryuiJS").Include(
                        "~/assets/js/jquery-ui.min.js"));

            // Importar el css del plugin UI de jquery
            bundles.Add(new StyleBundle("~/bundles/jqueryuiCss").Include(
                          "~/assets/css/jquery-ui.min.css"));

            // Importar el js del context menu
            bundles.Add(new ScriptBundle("~/bundles/ContextMenuJS").Include(
                        "~/assets/js/jquery.contextMenu.min.js"));

            // Importa el css del context menu
            bundles.Add(new StyleBundle("~/bundles/ContextMenuCSS").Include(
                        "~/assets/css/jquery.contextMenu.min.css"));

            // Importar el js para la vista materiales
            bundles.Add(new ScriptBundle("~/bundles/materialJS").Include(
                        "~/assets/js/Material.js"));

            // Importa el js para la vista de facturas
            bundles.Add(new ScriptBundle("~/bundles/facturaJS").Include(
                        "~/assets/js/Factura.js"));

            // Importar el js para los dropdown especificamente
            bundles.Add(new ScriptBundle("~/bundles/dropdownJS").Include(
                        "~/assets/Semantic UI/components/dropdown.js"));

            // Importa el css para el dropdown especificametne
            bundles.Add(new StyleBundle("~/bundles/dropdownCSS").Include(
                        "~/assets/Semantic UI/components/dropdown.css"));

            // Importa el js de plugin jquery para el typeahead
            bundles.Add(new ScriptBundle("~/bundles/jqueryTypeaheadJS").Include(
                        "~/assets/jquery/jquery-typeahead/jquery.typeahead.min.js"));

            // Importa el css de plugin de jquery para el typeahead
            bundles.Add(new StyleBundle("~/bundles/jqueryTypeaheadCSS").Include(
                        "~/assets/jquery/jquery-typeahead/jquery.typeahead.min.css"));

            // Importa el css de bootstrap
            bundles.Add(new StyleBundle("~/bundles/bootstrapCSS").Include(
                        "~/assets/css/bootstrap.min.css"));

            // Importa el css del estilo del PDF
            bundles.Add(new StyleBundle("~/bundles/estiloPDFCSS").Include(
                        "~/assets/css/estiloPDF.css"));

            // Importa el css del preload
            bundles.Add(new StyleBundle("~/bundles/preloadCSS").Include(
                        "~/assets/Preload/css/preloader.css"));

            // Importa el js del preload
            bundles.Add(new ScriptBundle("~/bundles/preloadJS").Include(
                            "~/assets/Preload/js/jquery.preloader.min.js"));
        }
    }
}
