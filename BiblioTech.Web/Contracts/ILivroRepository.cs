using BiblioTech.Web.Models;

namespace BiblioTech.Web.Contracts;

public interface ILivroRepository
{
    List<Livro> ObterTodos();
    Livro? ObterPorId(int id);
    Livro Adicionar(Livro livro, IEnumerable<int> autoresIds);
    void Atualizar(Livro livro, IEnumerable<int> autoresIds);
    void AtualizarEstoque(int livroId, int novaQuantidade);
    void Remover(int id);
}
