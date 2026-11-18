using BiblioTech.Web.Models;

namespace BiblioTech.Web.Contracts;

public interface IAutorRepository
{
    List<Autor> ObterTodos();
    Autor? ObterPorId(int id);
    Autor Adicionar(Autor autor);
}
