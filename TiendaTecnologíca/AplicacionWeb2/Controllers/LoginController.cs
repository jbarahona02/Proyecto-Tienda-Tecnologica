using AplicacionWeb2.Models.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb2.Entities;

namespace AplicacionWeb2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginView()
        {
            if (Session["usuario"] != null)
            {
                return RedirectToAction("PrincipalView","Principal");
            }
            else
            {
                return View();
            }
        }
        

        /// <summary>
        /// Permite evaluar el login de usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,ValidateAntiForgeryToken]
        public JsonResult Login(LoginViewModel model) {
            String mensaje = "";
            int tipoMensaje = 2;

            if (!ModelState.IsValid)
            {
                mensaje = ModelState.Values.Select(e => e.Errors).Where(e => e.Count > 0).FirstOrDefault().Select(v => v.ErrorMessage).FirstOrDefault();
            }
            else {
                using (var db = new DBAplicacion2Entities()) {
                    var usuarioDB = db.Usuario.Where(us => us.correo == model.email && us.contrasenia == model.password).FirstOrDefault();

                    if (usuarioDB != null)
                    {
                        Session["usuario"] = usuarioDB;
                        tipoMensaje = 1;
                        mensaje = "Credenciales válidas";
                    }
                    else {
                        mensaje = "Credenciales inválidas";
                    }
                }
            }
            return Json (new { tipo = tipoMensaje, mensaje = mensaje},JsonRequestBehavior.AllowGet);
        }
    }
}