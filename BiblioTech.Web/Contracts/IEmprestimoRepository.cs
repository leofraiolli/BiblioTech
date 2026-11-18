using BiblioTech.Web.Models;

namespace BiblioTech.Web.Contracts;

public interface IEmprestimoRepository
{
    List<Emprestimo> ObterTodos();
    Emprestimo? ObterPorId(int id);
    Emprestimo Registrar(Emprestimo emprestimo);
    void MarcarComoDevolvido(int id, DateTime dataDevolucao);
    bool PossuiAtivoPorLivro(int livroId);
}
