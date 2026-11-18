using BiblioTech.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Web.Controllers;

public class EmprestimosController : Controller
{
    private readonly EmprestimoService _emprestimoService;
    private readonly LivroService _livroService;

    public EmprestimosController(EmprestimoService emprestimoService, LivroService livroService)
    {
        _emprestimoService = emprestimoService;
        _livroService = livroService;
    }

    public IActionResult Index()
    {
        var emprestimos = _emprestimoService.ListarTodos();
        return View(emprestimos);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Livros = _livroService.ListarTodos().Where(l => l.PossuiDisponibilidade()).ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(int livroId, string matriculaAluno, string nomeAluno)
    {
        try
        {
            _emprestimoService.RealizarEmprestimo(livroId, matriculaAluno, nomeAluno);
            TempData["Sucesso"] = "Empréstimo registrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            ViewBag.Livros = _livroService.ListarTodos().Where(l => l.PossuiDisponibilidade()).ToList();
            return View();
        }
    }

    [HttpPost]
    public IActionResult Devolver(int id)
    {
        try
        {
            _emprestimoService.RegistrarDevolucao(id);
            TempData["Sucesso"] = "Devolução registrada e estoque atualizado!";
        }
        catch (Exception ex)
        {
            TempData["Erro"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
