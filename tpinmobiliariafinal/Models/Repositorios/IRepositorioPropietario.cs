using tpinmobiliariafinal.Models.Objetos;
using System.Collections.Generic;

namespace tpinmobiliariafinal.Models.Repositorios
{
	public interface IRepositorioPropietario : IRepositorio<Propietario>
	{
		Propietario ObtenerPorEmail(string email);
		IList<Propietario> BuscarPorNombre(string nombre);
	}
}
