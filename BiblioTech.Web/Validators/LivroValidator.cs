using BiblioTech.Web.Contracts;
using BiblioTech.Web.Models;

namespace BiblioTech.Web.Validators;

public class LivroValidator : ILivroValidator
{
    public void Validar(Livro livro, IEnumerable<int> autoresIds)
    {
        if (string.IsNullOrWhiteSpace(livro.Titulo))
            throw new ArgumentException("O título do livro é obrigatório.");

        if (string.IsNullOrWhiteSpace(livro.Isbn))
            throw new ArgumentException("O código ISBN do livro é obrigatório.");

        if (livro.AnoPublicacao < 1450 || livro.AnoPublicacao > DateTime.Now.Year + 1)
            throw new ArgumentException("O ano de publicação informado é inválido.");

        if (livro.QuantidadeDisponivel < 0)
            throw new ArgumentException("A quantidade disponível não pode ser negativa.");

        if (autoresIds is null || !autoresIds.Distinct().Any())
            throw new ArgumentException("Selecione pelo menos um autor para vincular ao livro (relação N:N).");
    }
}
