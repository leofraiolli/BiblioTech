namespace BiblioTech.Web.Models;

public class Autor
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Nacionalidade { get; set; } = string.Empty;

    // Navegação N:N para os livros associados a este autor
    public List<LivroAutor> LivroAutores { get; set; } = new();

    public int TotalObras() => LivroAutores.Count;
}
