using AplicacionWeb2.Entities;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWeb2.Controllers
{
    public class FacturaController : Controller
    {
        // GET: Factura

        /// <summary>
        /// Método que permite devolver la vista y generar los ViewBag  para poder llenar los dropdown en la vista
        /// </summary>
        /// <returns></returns>
        public ActionResult FacturaView()
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("LoginView", "Login");
            }

            // using para poder acceder a la tabla de cliente
            using (var db2 = new DBAplicacion2Entities5())
            {
                ViewBag.clientes = db2.Cliente.ToList();
            }

            // using para poder acceder a la tabla materiales
            using (var db3 = new DBAplicacion2Entities3()) {
                ViewBag.materiales = db3.Materiales.ToList();
            }
            return View();

        }


        /// <summary>
        /// Método que permite recuperar un cliente de la lista segun el nit seleccionado
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        public JsonResult PeticionCliente(String nit) {
            using (var db2 = new DBAplicacion2Entities5())
            {
                ViewBag.clientes = db2.Cliente.ToList();
            }

            List<Cliente> clientes = ViewBag.clientes;
            var client = clientes.Where(c => c.NIT == nit).FirstOrDefault();

            return Json(new { client = client }, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// Permite devolver las facturas tanto con el encabezado, el detalle y el cliente
        /// </summary>
        /// <returns></returns>
        public ActionResult ListarFacturasView() {
            using (var db = new DBAplicacion2Entities4())
            {
                var listaFacturas = db.EncabezadoFactura.Include("Cliente").Include("DetalleFactura.Materiales").ToList();
                Session["facturas"] = listaFacturas;
                return View(listaFacturas);
            }
        }


        /// <summary>
        /// Permite devolver la factura específica para el modelo del PDF
        /// </summary>
        /// <param name="codFac"></param>
        /// <returns></returns>
        public ActionResult FacturaPDFView(string codFac) {
            using (var db = new DBAplicacion2Entities4()) {
                var codigoFactura = Convert.ToInt32(codFac);
                var factura =db.EncabezadoFactura.Include("Cliente").Include("DetalleFactura.Materiales").Where(fac => fac.codigoFactura == codigoFactura).FirstOrDefault();
                return View(factura);
            }     
        }

        [HttpPost]
        public JsonResult GenerarReporte(string codFac) {
            var valorPDF = 1;
            //Devuelve la ruta de la aplicacion
            String contentRoot = Url.Content("~/");
            String applicationRoot = Request.Url.Scheme + "://" + Request.Url.Authority + contentRoot.Substring(0, contentRoot.Length - 1);
            var path = String.Format("{0}/Factura/FacturaPDFView?codFac={1}", applicationRoot, codFac);

            try
            {
                HtmlToPdf htmlPDF = new HtmlToPdf();
                PdfDocument pdf = htmlPDF.ConvertUrl(path);

                pdf.Save(String.Format("{0}PDF/Factura.pdf", Server.MapPath("~/")));
                pdf.Close();
            }catch(Exception ){
                valorPDF = 0;
            }
            return Json( new { codigo = valorPDF},JsonRequestBehavior.DenyGet);
        }


        public JsonResult CrearFactura(string nit, string fec, List<int> codPos, List<int> codMat, List<int> cantidad) {
            string men = "";
            using (var db = new DBAplicacion2Entities4())
            {
                var encabezado = new EncabezadoFactura();
                encabezado.nit = nit;
                encabezado.fecha = fec;
                db.EncabezadoFactura.Add(encabezado);
                db.SaveChanges();
   
                for(int i = 0; i < codPos.Count; i++) {
                    var detalle = new DetalleFactura();
                    detalle.codigoPosicion = codPos[i];
                    detalle.codigoFactura = encabezado.codigoFactura;
                    detalle.codigoMaterial = codMat[i];
                    detalle.cantidad = cantidad[i];
                    db.DetalleFactura.Add(detalle);
                    db.SaveChanges();
                    men = "Se a guardado con éxito la factura";
                }
            }
            return Json(new { mensaje = men},JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Permite devolver el material con respecto al codigo elegido en la vista
        /// </summary>
        /// <param name="codigoMat"></param>
        /// <returns></returns>
        public JsonResult PeticionMaterial(String codigoMat) {
            using (var db = new DBAplicacion2Entities3()) {
                ViewBag.materiales = db.Materiales.ToList();
            }

            var codigo = Convert.ToInt32(codigoMat);
            List<Materiales> materiales = ViewBag.materiales;
            var material = materiales.Where(mat => mat.codigoMaterial == codigo).FirstOrDefault();

            return Json(new { material = material }, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// Método que permite lista los materiales completos para llenar el view bag para pedirlos de lado del js
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ListaMateriales() {
            using (var db = new DBAplicacion2Entities3()) {
                ViewBag.mat = db.Materiales.ToList();
            }

            List<Materiales> listaMat = ViewBag.mat;
            return Json(new {lista = listaMat }, JsonRequestBehavior.AllowGet);
        }
    }
}
