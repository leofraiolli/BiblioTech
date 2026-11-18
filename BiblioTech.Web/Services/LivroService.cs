using BiblioTech.Web.Contracts;
using BiblioTech.Web.Models;

namespace BiblioTech.Web.Services;

public class LivroService
{
    private readonly ILivroRepository _livroRepository;
    private readonly IEmprestimoRepository _emprestimoRepository;
    private readonly ILivroValidator _validator;

    public LivroService(
        ILivroRepository livroRepository,
        IEmprestimoRepository emprestimoRepository,
        ILivroValidator validator)
    {
        _livroRepository = livroRepository;
        _emprestimoRepository = emprestimoRepository;
        _validator = validator;
    }

    public List<Livro> ListarTodos() => _livroRepository.ObterTodos();

    public Livro? BuscarPorId(int id) => _livroRepository.ObterPorId(id);

    public Livro CadastrarLivro(Livro livro, IEnumerable<int> autoresIds)
    {
        var ids = autoresIds?.ToList() ?? new List<int>();
        _validator.Validar(livro, ids);
        return _livroRepository.Adicionar(livro, ids);
    }

    public void AtualizarLivro(Livro livro, IEnumerable<int> autoresIds)
    {
        var ids = autoresIds?.ToList() ?? new List<int>();
        _validator.Validar(livro, ids);
        _livroRepository.Atualizar(livro, ids);
    }

    public void ExcluirLivro(int id)
    {
        if (_emprestimoRepository.PossuiAtivoPorLivro(id))
            throw new InvalidOperationException("Não é possível excluir um livro que possui empréstimos em aberto.");

        _livroRepository.Remover(id);
    }
}
