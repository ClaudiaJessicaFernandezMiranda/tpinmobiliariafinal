using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tpinmobiliariafinal.Models.Objetos
{
	public class Inmueble
	{
		[Display(Name = "Código")]
		public int Id { get; set; }
		[Required]
		public string Direccion { get; set; }
		[Required]
		public int Ambientes { get; set; }
		[Required]
		public string Tipo { get; set; }
		[Required]
		public string Uso { get; set; }
		public int Superficie { get; set; }
		public decimal Latitud { get; set; }
		public decimal Longitud { get; set; }
		public int Estado { get; set; }
		[Required]
		public int IdPropietario { get; set; }
		[ForeignKey("IdPropietario")]
		[Display(Name = "Dueño")]
		public Propietario Propietario { get; set; }
	}
}

