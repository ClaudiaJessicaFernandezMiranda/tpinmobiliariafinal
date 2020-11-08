using tpinmobiliariafinal.Models.Objetos;
using System.Collections.Generic;

namespace tpinmobiliariafinal.Models.Repositorios
{
    public interface IRepositorioInmueble : IRepositorio<Inmueble>
    {
        IList<Inmueble> BuscarPorPropietario(int idPropietario);
    }
}
