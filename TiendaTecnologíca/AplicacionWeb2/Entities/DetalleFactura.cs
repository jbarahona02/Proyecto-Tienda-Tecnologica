//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplicacionWeb2.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleFactura
    {
        public int codigoPosicion { get; set; }
        public int codigoFactura { get; set; }
        public Nullable<int> codigoMaterial { get; set; }
        public Nullable<int> cantidad { get; set; }
    
        public virtual EncabezadoFactura EncabezadoFactura { get; set; }
        public virtual Materiales Materiales { get; set; }
    }
}
