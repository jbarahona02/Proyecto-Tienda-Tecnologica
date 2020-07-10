using AplicacionWeb2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb2.Models.ViewModels.ClienteModels;
using System.Data.SqlClient;

namespace AplicacionWeb2.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente

        /// <summary>
        /// Método que permite retornar la vista de clientes y como recibe dos modelos que los uno en una clase le mando ahi el cliente
        /// vacío y la lista llena de la lista de clientes
        /// </summary>
        /// <returns></returns>
        public ActionResult ClienteView()
        {
            if (Session["usuario"] == null) {
                return RedirectToAction("LoginView","Login");
            }
            using (var db = new DBAplicacion2Entities5()) {
                var model = new ClienteModel();
                model.cliente = new Cliente();
                model.listaClientes = db.Cliente.ToList();

                return View(model);
            }
        }

        // Método que retornara la vista para el modal y modificar el ususario

        /// <summary>
        /// Método que retorna la vista parcial con modelo con datos por ello es que le mando un cliente según el código seleccionado
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        public ActionResult ModificarClienteView(string nit ) {
            using (var db = new DBAplicacion2Entities5()) {
                var clienteDB = db.Cliente.Where(cliente => cliente.NIT == nit).FirstOrDefault();
                return View(clienteDB);
            }
        }

        // Va a permitir crear un nuevo cliente

        /// <summary>
        /// Método que va a permitir crear un nuevo registro
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Crear(Cliente cliente)
        {
            string mensaje = "";
            int tipoMensaje = 2;

            if (!ModelState.IsValid)
            {
                mensaje = ModelState.Values.Select(e => e.Errors).Where(e => e.Count > 0).FirstOrDefault().Select(m => m.ErrorMessage).FirstOrDefault();
            }
            else {
                if (ValidarNit(cliente.NIT))
                {               
                        using (var db = new DBAplicacion2Entities5())
                        {
                        
                            try
                            {
                                tipoMensaje = 1;
                                mensaje = "Se a creado el cliente con éxito";
                                db.Cliente.Add(cliente);
                                db.SaveChanges();
                            }
                            catch (Exception)
                            {
                                mensaje = "El NIT que ingreso ya existe en la DB";
                            }
                        }
                }
                else {
                    mensaje = "Debe de ingresar un Nit válido";
                }
            }

            return Json(new { tipo = tipoMensaje, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        // Va a permitir modificar un usuario
        /// <summary>
        /// Método que permite modificar un usuario
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public JsonResult Modificar(Cliente cliente)
        {
            string mensaje = "";
            int tipoMensaje = 2;

            if (!ModelState.IsValid)
            {
                mensaje = ModelState.Values.Select(errores => errores.Errors).Where(errores => errores.Count > 0).FirstOrDefault().Select(men => men.ErrorMessage).FirstOrDefault();
            }
            else {
                if (validarCambio(cliente.NIT, cliente)) {
                    if (ValidarNit(cliente.NIT))
                    {
                        using (var db = new DBAplicacion2Entities5())
                        {
                            tipoMensaje = 1;
                            db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            mensaje = "Se a actualizado el cliente con éxito";
                        }
                    }
                    else {
                        mensaje = "Ingrese un NIT válido";
                    }
                }
                else {
                    tipoMensaje = 1;
                    mensaje = "No se actualizo el usuario por que aún no hay cambios";
                }
            }

            return Json(new { tipo = tipoMensaje, men = mensaje }, JsonRequestBehavior.DenyGet);
        }


        /// <summary>
        /// Mètodo que sirve para validar si hubieron cambios con los datos que estan guardados en la DB para no guardar lo mismo y consumir recursos
        /// </summary>
        /// <param name="codCliente"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public Boolean validarCambio(string nit, Cliente cliente) {
            using (var db = new DBAplicacion2Entities5()) {
                var clienteDB = db.Cliente.Where(cli => cli.NIT == nit).FirstOrDefault();

                foreach (var propiedad in cliente.GetType().GetProperties().Where(prop => prop.PropertyType == typeof(string))) {
                    var propiedadDB = clienteDB.GetType().GetProperty(propiedad.Name);
                    var propiedadCli = cliente.GetType().GetProperty(propiedad.Name);

                    if ((propiedadDB.GetValue(clienteDB).ToString()) != (propiedadCli.GetValue(cliente).ToString()) ) {
                        return true;
                    }
                }
            }
                return false;
        }

        // Este método sera para poder eliminar un usuario
        public JsonResult eliminar(String nit) {
            using (var db = new DBAplicacion2Entities5()) {
                var cliente = db.Cliente.Where(cli => cli.NIT == nit).FirstOrDefault();
                db.Cliente.Remove(cliente);
                db.SaveChanges();
                var mensaje = "Se a eliminado el registro";
                return Json(new { men = mensaje}, JsonRequestBehavior.DenyGet);
                } 
        }


        /// <summary>
        /// Mètodo que permite validar el NIT
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        public Boolean ValidarNit(string nit)
        {
            Boolean validacion = false;
            if (nit.ToUpper().Equals("CF") || nit.ToUpper().Equals("C/F"))
            {
                validacion = true;
            } else if (nit.Length == 8 && nit.Substring(6,1).Equals("-")){
                var n1 = Convert.ToInt32(nit.Substring(5,1)) * 2;
                var n2 = Convert.ToInt32(nit.Substring(4,1)) * 3;
                var n3 = Convert.ToInt32(nit.Substring(3,1)) * 4;
                var n4 = Convert.ToInt32(nit.Substring(2,1)) * 5;
                var n5 = Convert.ToInt32(nit.Substring(1,1)) * 6;
                var n6 = Convert.ToInt32(nit.Substring(0,1)) * 7;

                var suma = n1 + n2 + n3 + n4 + n5 + n6;

                var residuo = suma % 11;
                var residuo2 = residuo % 11;

                var resultado = 11 - residuo2;

                var valorK = nit.Substring(7,1);

                if (valorK.ToUpper().Equals("K"))
                {
                    if (resultado == 10)
                    {
                        validacion = true;
                    }
                }
                else{
                    var valor = Convert.ToInt32(nit.Substring(7,1));

                    if (valor == resultado) {
                        validacion = true;
                    }
                }


            }else if(nit.Length == 9 && nit.Substring(7,1).Equals("-")) {
                var n1 = Convert.ToInt32(nit.Substring(6,1)) * 2;
                var n2 = Convert.ToInt32(nit.Substring(5,1)) * 3;
                var n3 = Convert.ToInt32(nit.Substring(4,1)) * 4;
                var n4 = Convert.ToInt32(nit.Substring(3,1)) * 5;
                var n5 = Convert.ToInt32(nit.Substring(2,1)) * 6;
                var n6 = Convert.ToInt32(nit.Substring(1,1)) * 7;
                var n7 = Convert.ToInt32(nit.Substring(0,1)) * 8;

                var suma = n1 + n2 + n3 + n4 + n5 + n6 + n7;

                var residuo = suma % 11;
                var residuo2 = residuo % 11;

                var resultado = 11 - residuo2;

                var valorK = nit.Substring(8,1);

                if (valorK.ToUpper().Equals("K"))
                {
                    if (resultado == 10)
                    {
                        validacion = true;
                    }
                }
                else {
                    var valor = Convert.ToInt32(nit.Substring(8, 1));
                    if (valor == resultado)
                    {
                        validacion = true;
                    }
                }
            }
                return validacion;
        }

    }
}