using AplicacionWeb2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace AplicacionWeb2.Models.ViewModels.MaterialModels
{
    public class MaterialModelView
    {
        public Materiales material = new Materiales();
        public List<Materiales> listaMateriales = new List<Materiales>();
    }
}