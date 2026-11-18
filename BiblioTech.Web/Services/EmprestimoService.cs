using BiblioTech.Web.Contracts;
using BiblioTech.Web.Models;

namespace BiblioTech.Web.Services;

public class EmprestimoService
{
    private readonly IEmprestimoRepository _emprestimoRepository;
    private readonly ILivroRepository _livroRepository;

    public EmprestimoService(
        IEmprestimoRepository emprestimoRepository,
        ILivroRepository livroRepository)
    {
        _emprestimoRepository = emprestimoRepository;
        _livroRepository = livroRepository;
    }

    public List<Emprestimo> ListarTodos() => _emprestimoRepository.ObterTodos();

    public Emprestimo RealizarEmprestimo(int livroId, string matriculaAluno, string nomeAluno)
    {
        if (string.IsNullOrWhiteSpace(matriculaAluno))
            throw new ArgumentException("A matrícula institucional do aluno é obrigatória.");

        if (string.IsNullOrWhiteSpace(nomeAluno))
            throw new ArgumentException("O nome do aluno é obrigatório.");

        var livro = _livroRepository.ObterPorId(livroId)
            ?? throw new KeyNotFoundException("Livro selecionado não encontrado.");

        if (!livro.PossuiDisponibilidade())
            throw new InvalidOperationException("Não há exemplares disponíveis deste livro para empréstimo.");

        _livroRepository.AtualizarEstoque(livro.Id, livro.QuantidadeDisponivel - 1);

        var emprestimo = new Emprestimo
        {
            LivroId = livro.Id,
            Livro = livro,
            MatriculaAluno = matriculaAluno.Trim(),
            NomeAluno = nomeAluno.Trim(),
            DataEmprestimo = DateTime.Now,
            DataPrevistaDevolucao = DateTime.Now.AddDays(7)
        };

        return _emprestimoRepository.Registrar(emprestimo);
    }

    public void RegistrarDevolucao(int emprestimoId)
    {
        var emprestimo = _emprestimoRepository.ObterPorId(emprestimoId)
            ?? throw new KeyNotFoundException("Registro de empréstimo não encontrado.");

        if (emprestimo.Devolvido)
            throw new InvalidOperationException("Este empréstimo já foi devolvido anteriormente.");

        _emprestimoRepository.MarcarComoDevolvido(emprestimoId, DateTime.Now);

        var livro = _livroRepository.ObterPorId(emprestimo.LivroId);
        if (livro is not null)
        {
            _livroRepository.AtualizarEstoque(livro.Id, livro.QuantidadeDisponivel + 1);
        }
    }
}
