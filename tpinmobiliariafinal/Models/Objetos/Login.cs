using System.ComponentModel.DataAnnotations;

namespace tpinmobiliariafinal.Models.Objetos
{
    public class Login
    {
        [Required(ErrorMessage = "Debe ingresar un usuario")]
        [StringLength(30, ErrorMessage = "Ingrese usuario de minimo 3 y maximo 30", MinimumLength = 3)]
        [DataType(DataType.EmailAddress)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar contraseña")]
        [StringLength(30, ErrorMessage = "Ingrese password de minimo 3 y maximo 30", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
