using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tpinmobiliariafinal.Models.Objetos
{
    public class Contrato
    {
        public int Id { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }

        [Required]
        public DateTime FechaBaja { get; set; }

        [Required]
        public double Monto { get; set; }

        public string Descripcion { get; set; }

        [Display(Name = "Propiedad")]
        public int IdInmueble { get; set; }
        [ForeignKey("IdInmueble")]
        public Inmueble inmueble { get; set; }

        [Display(Name = "Inquilino")]
        public int IdInquilino { get; set; }
        [ForeignKey("IdInquilino")]
        public Inquilino inquilino { get; set; }

    }
}
