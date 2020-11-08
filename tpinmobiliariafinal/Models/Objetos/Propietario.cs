using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tpinmobiliariafinal.Models.Objetos
{
    public class Propietario
    {
        [Key]
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Required]
        public string Dni { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Telefono { get; set; }

        [Required, EmailAddress]
        public string Mail { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}