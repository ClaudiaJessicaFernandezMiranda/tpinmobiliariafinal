using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tpinmobiliariafinal.Models.Objetos
{
    public class Pago
    {
        public int PagoId { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [Display(Name = "Cuota")]
        public string Importe { get; set; }
        public int IdAlquiler { get; set; }
        [ForeignKey("IdAlquiler")]
        public Contrato contrato { get; set; }
    }
}
