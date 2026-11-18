using BiblioTech.Web.Contracts;
using BiblioTech.Web.Models;

namespace BiblioTech.Web.Repositories;

public class InMemoryLivroRepository : ILivroRepository
{
    private readonly List<Livro> _livros = new();
    private readonly IAutorRepository _autorRepository;
    private int _nextId = 1;

    public InMemoryLivroRepository(IAutorRepository autorRepository, bool popularDadosIniciais = true)
    {
        _autorRepository = autorRepository;

        if (popularDadosIniciais)
        {
            Adicionar(new Livro
            {
                Titulo = "Dom Casmurro",
                Isbn = "978-8535914849",
                AnoPublicacao = 1899,
                QuantidadeDisponivel = 4
            }, new[] { 1 });

            // Exemplo nativo de relacionamento N:N (um livro com múltiplos coautores)
            Adicionar(new Livro
            {
                Titulo = "Padrões de Projetos: Soluções Reutilizáveis",
                Isbn = "978-8573076103",
                AnoPublicacao = 2000,
                QuantidadeDisponivel = 2
            }, new[] { 4, 5 });
        }
    }

    public List<Livro> ObterTodos() => _livros.OrderBy(l => l.Titulo).ToList();

    public Livro? ObterPorId(int id) => _livros.FirstOrDefault(l => l.Id == id);

    public Livro Adicionar(Livro livro, IEnumerable<int> autoresIds)
    {
        livro.Id = _nextId++;
        SincronizarAutores(livro, autoresIds);
        _livros.Add(livro);
        return livro;
    }

    public void Atualizar(Livro livro, IEnumerable<int> autoresIds)
    {
        var existente = ObterPorId(livro.Id)
            ?? throw new KeyNotFoundException("Livro não encontrado no acervo.");

        existente.Titulo = livro.Titulo;
        existente.Isbn = livro.Isbn;
        existente.AnoPublicacao = livro.AnoPublicacao;
        existente.QuantidadeDisponivel = livro.QuantidadeDisponivel;

        // Remove vínculos antigos na navegação dos autores antes de recriar a relação N:N
        foreach (var antigoVinculo in existente.LivroAutores)
        {
            antigoVinculo.Autor?.LivroAutores.RemoveAll(la => la.LivroId == existente.Id);
        }

        SincronizarAutores(existente, autoresIds);
    }

    public void AtualizarEstoque(int livroId, int novaQuantidade)
    {
        var livro = ObterPorId(livroId)
            ?? throw new KeyNotFoundException("Livro não encontrado.");
        livro.QuantidadeDisponivel = novaQuantidade;
    }

    public void Remover(int id)
    {
        var livro = ObterPorId(id);
        if (livro is null) return;

        foreach (var vinculo in livro.LivroAutores)
        {
            vinculo.Autor?.LivroAutores.RemoveAll(la => la.LivroId == id);
        }

        _livros.Remove(livro);
    }

    private void SincronizarAutores(Livro livro, IEnumerable<int> autoresIds)
    {
        livro.LivroAutores.Clear();

        foreach (var autorId in autoresIds.Distinct())
        {
            var autor = _autorRepository.ObterPorId(autorId)
                ?? throw new ArgumentException($"Autor com ID {autorId} não encontrado.");

            var associacao = new LivroAutor
            {
                LivroId = livro.Id,
                Livro = livro,
                AutorId = autor.Id,
                Autor = autor
            };

            livro.LivroAutores.Add(associacao);
            autor.LivroAutores.Add(associacao);
        }
    }
}
