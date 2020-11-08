
using tpinmobiliariafinal.Models.Objetos;

namespace tpinmobiliariafinal.Models.Repositorios
{
	public interface IRepositorioInquilino : IRepositorio<Inquilino>
	{
		Inquilino ObtenerPorEmail(string email);
	}
}
