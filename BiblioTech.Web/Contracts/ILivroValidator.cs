using BiblioTech.Web.Models;

namespace BiblioTech.Web.Contracts;

public interface ILivroValidator
{
    void Validar(Livro livro, IEnumerable<int> autoresIds);
}
