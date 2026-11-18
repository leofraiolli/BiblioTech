using BiblioTech.Web.Contracts;
using BiblioTech.Web.Models;

namespace BiblioTech.Web.Repositories;

public class InMemoryEmprestimoRepository : IEmprestimoRepository
{
    private readonly List<Emprestimo> _emprestimos = new();
    private int _nextId = 1;

    public List<Emprestimo> ObterTodos() =>
        _emprestimos.OrderByDescending(e => e.DataEmprestimo).ToList();

    public Emprestimo? ObterPorId(int id) =>
        _emprestimos.FirstOrDefault(e => e.Id == id);

    public Emprestimo Registrar(Emprestimo emprestimo)
    {
        emprestimo.Id = _nextId++;
        _emprestimos.Add(emprestimo);
        return emprestimo;
    }

    public void MarcarComoDevolvido(int id, DateTime dataDevolucao)
    {
        var emprestimo = ObterPorId(id)
            ?? throw new KeyNotFoundException("Empréstimo não encontrado.");
        emprestimo.DataDevolucao = dataDevolucao;
    }

    public bool PossuiAtivoPorLivro(int livroId) =>
        _emprestimos.Any(e => e.LivroId == livroId && !e.Devolvido);
}
