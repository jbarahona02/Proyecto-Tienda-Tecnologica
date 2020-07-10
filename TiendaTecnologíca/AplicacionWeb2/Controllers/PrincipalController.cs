using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWeb2.Controllers
{
    public class PrincipalController : Controller
    {
        // GET: Principal
        public ActionResult PrincipalView()
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("LoginView","Login");
            }
            else {
                return View();
            }

        }

        // Método que va a cerrar la sesión de usuario
        public ActionResult CerrarSesion() {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("LoginView","Login");
        }
    }
}