namespace BiblioTech.Web.Models;

public class Emprestimo
{
    public int Id { get; set; }
    public int LivroId { get; set; }
    public Livro? Livro { get; set; }

    // Identificação escolar sem exposição de dados pessoais sensíveis
    public string MatriculaAluno { get; set; } = string.Empty;
    public string NomeAluno { get; set; } = string.Empty;

    public DateTime DataEmprestimo { get; set; } = DateTime.Now;
    public DateTime DataPrevistaDevolucao { get; set; } = DateTime.Now.AddDays(7);
    public DateTime? DataDevolucao { get; set; }

    public bool Devolvido => DataDevolucao.HasValue;
}
