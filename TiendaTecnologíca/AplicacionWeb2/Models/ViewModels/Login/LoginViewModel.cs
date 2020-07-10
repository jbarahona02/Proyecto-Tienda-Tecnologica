using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionWeb2.Models.ViewModels.Login
{
    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Ingrese un correo")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Ingrese un correo válido")]
        public string email { get; set;}

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Ingrese una contraseña")]
        public string password { get; set;}
    }
}