using BiblioTech.Web.Contracts;
using BiblioTech.Web.Models;

namespace BiblioTech.Web.Repositories;

public class InMemoryAutorRepository : IAutorRepository
{
    private readonly List<Autor> _autores = new();
    private int _nextId = 1;

    public InMemoryAutorRepository(bool popularDadosIniciais = true)
    {
        if (popularDadosIniciais)
        {
            Adicionar(new Autor { Nome = "Machado de Assis", Nacionalidade = "Brasileira" });
            Adicionar(new Autor { Nome = "Clarice Lispector", Nacionalidade = "Brasileira" });
            Adicionar(new Autor { Nome = "Robert C. Martin", Nacionalidade = "Norte-americana" });
            Adicionar(new Autor { Nome = "Erich Gamma", Nacionalidade = "Suíça" });
            Adicionar(new Autor { Nome = "Richard Helm", Nacionalidade = "Australiana" });
        }
    }

    public List<Autor> ObterTodos() => _autores.OrderBy(a => a.Nome).ToList();

    public Autor? ObterPorId(int id) => _autores.FirstOrDefault(a => a.Id == id);

    public Autor Adicionar(Autor autor)
    {
        autor.Id = _nextId++;
        _autores.Add(autor);
        return autor;
    }
}
