using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb2.Entities;
using AplicacionWeb2.Models.ViewModels.MaterialModels;

namespace AplicacionWeb2.Controllers
{
    public class MaterialController : Controller
    {
        // GET: Material
        
        /// <summary>
        /// Método que permite retornar el modelo que une ambos modelos tanto el material vacio y la lista
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterialView()
        {
            using (var db = new DBAplicacion2Entities3()) {
                if (Session["usuario"] == null)
                {
                    return RedirectToAction("LoginView", "Login");
                }
                else
                {
                    MaterialModelView model = new MaterialModelView();
                    model.material = new Materiales();
                    model.listaMateriales = db.Materiales.ToList();
                    return View(model);
                }
            }
        }

        // Método que va a retornar la vista con el modelo lleno 

        /// <summary>
        /// Método que permite devolver la vista parcial con un modelo lleno por ello le mando el cliente segun el código que le ingrese
        /// </summary>
        /// <param name="codMat"></param>
        /// <returns></returns>
        public ActionResult ModificarMaterialView(int codMat) {
            using (var db = new DBAplicacion2Entities3()) {
                var modeloMaterial = db.Materiales.Where(mat => mat.codigoMaterial == codMat).FirstOrDefault();
                return View(modeloMaterial);
            }
        }

        // Método que se encargara de crear un nuevo material

        /// <summary>
        /// Método que permite crear un nuevo material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult crearMaterial(Materiales material) {
            string mensaje = "";
            int tipoMensaje = 2;

            if (!ModelState.IsValid)
            {
                mensaje = ModelState.Values.Select(e => e.Errors).Where(e => e.Count > 0).FirstOrDefault().Select(error => error.ErrorMessage).FirstOrDefault();
            }
            else {
                using (var db = new DBAplicacion2Entities3()) {
                    tipoMensaje = 1;
                    mensaje = "Se a creado el nuevo material";
                    db.Materiales.Add(material);
                    db.SaveChanges();
                }
            }
            return Json(new { tipo = tipoMensaje, mensaje = mensaje }, JsonRequestBehavior.DenyGet);
        }



        // Método que permitira modificar un material

        /// <summary>
        /// Método que permite modificar o actualizar un material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public JsonResult ModificarMaterial(Materiales material) {
            string mensaje = "";
            int tipoMensaje = 2;

            if (!ModelState.IsValid)
            {
                mensaje = ModelState.Values.Select(errores => errores.Errors).Where(errores => errores.Count > 0).FirstOrDefault().Select(error => error.ErrorMessage).FirstOrDefault();
            }
            else {
                tipoMensaje = 1;
                if (validarCambios(material.codigoMaterial, material))
                {
                    using (var db = new DBAplicacion2Entities3())
                    {
                        db.Entry(material).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        mensaje = "Se a actualizado el material";

                    }
                }
                else {
                    mensaje = "No se a actualizado el material por que no han habido cambios";
                }
            }
            return Json(new {tipo = tipoMensaje, men = mensaje}, JsonRequestBehavior.DenyGet);
        }

        // Método que permitira determinar si hubieron cambios en los campos del material para actualizar o no

        /// <summary>
        /// Método que permite verificar que hubieron cambios en los datos para actualizarlos o no
        /// </summary>
        /// <param name="codigoMat"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        public Boolean validarCambios(int codigoMat, Materiales material) {
            using (var db = new DBAplicacion2Entities3()) {
                var materialDB = db.Materiales.Where(mat => mat.codigoMaterial == codigoMat).FirstOrDefault();

                foreach (var prop in material.GetType().GetProperties().Where(p => p.PropertyType == typeof(string))) {
                    var propiedadDB = materialDB.GetType().GetProperty(prop.Name);
                    var propiedadParam = material.GetType().GetProperty(prop.Name);

                    if ((propiedadDB.GetValue(materialDB).ToString()) != (propiedadParam.GetValue(material).ToString())) {
                        return true;
                    }
                }
            }
                return false;
        }
             

        // Método que permitira eliminar el registro
        
        /// <summary>
        /// Método que permite eliminar un material
        /// </summary>
        /// <param name="codMat"></param>
        /// <returns></returns>
        public JsonResult eliminarMaterial(String codMat) {
            string mensaje = "";

            try
            {
                using (var db = new DBAplicacion2Entities3())
                {
                    int codigo = Convert.ToInt32(codMat);
                    var materialDB = db.Materiales.Where(mat => mat.codigoMaterial == codigo).FirstOrDefault();
                    db.Materiales.Remove(materialDB);
                    db.SaveChanges();
                    mensaje = "Se a eliminado el registro con éxito";

                   
                }
            }
            catch (Exception) {
                mensaje = "No se puede eliminar el registro";
            }
            return Json(new { men = mensaje }, JsonRequestBehavior.DenyGet);
        }
    }
}