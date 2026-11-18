using BiblioTech.Web.Contracts;
using BiblioTech.Web.Models;

namespace BiblioTech.Web.Services;

public class AutorService
{
    private readonly IAutorRepository _autorRepository;

    public AutorService(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public List<Autor> ListarTodos() => _autorRepository.ObterTodos();

    public Autor CadastrarAutor(string nome, string nacionalidade)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do autor é obrigatório.");

        var autor = new Autor
        {
            Nome = nome.Trim(),
            Nacionalidade = string.IsNullOrWhiteSpace(nacionalidade) ? "Não informada" : nacionalidade.Trim()
        };

        return _autorRepository.Adicionar(autor);
    }
}
