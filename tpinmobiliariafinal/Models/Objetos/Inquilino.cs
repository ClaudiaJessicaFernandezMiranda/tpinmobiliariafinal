using System.ComponentModel.DataAnnotations;

namespace tpinmobiliariafinal.Models.Objetos
{
	public class Inquilino
	{
		[Key]
		[Display(Name = "Código")]
		public int Id { get; set; }
		[Required]
		public string Dni { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]	
		public string Telefono { get; set; }
		[Required, EmailAddress]
		public string Mail { get; set; }
	}
}
