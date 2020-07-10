using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AplicacionWeb2.Entities;
namespace AplicacionWeb2.Models.ViewModels.ClienteModels
{
    public class ClienteModel
    {

        // Ambos objetos son para unir dos modelos un modelo de cliente que ya había generado y además una lista por que necesito ambos.
        public Cliente cliente = new Cliente();
        public List<Cliente> listaClientes = new List<Cliente>();
    }
}