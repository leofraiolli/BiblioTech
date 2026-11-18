namespace BiblioTech.Web.Models;

public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int AnoPublicacao { get; set; }
    public int QuantidadeDisponivel { get; set; }

    // Navegação N:N para múltiplos autores da obra
    public List<LivroAutor> LivroAutores { get; set; } = new();

    public bool PossuiDisponibilidade() => QuantidadeDisponivel > 0;

    public string NomesAutores()
    {
        if (LivroAutores.Count == 0)
            return "Sem autor vinculado";

        return string.Join(", ", LivroAutores
            .Where(la => la.Autor is not null)
            .Select(la => la.Autor!.Nome));
    }
}
