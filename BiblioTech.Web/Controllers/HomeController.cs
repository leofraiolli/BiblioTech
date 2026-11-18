using BiblioTech.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Web.Controllers;

public class HomeController : Controller
{
    private readonly LivroService _livroService;
    private readonly AutorService _autorService;
    private readonly EmprestimoService _emprestimoService;

    public HomeController(
        LivroService livroService,
        AutorService autorService,
        EmprestimoService emprestimoService)
    {
        _livroService = livroService;
        _autorService = autorService;
        _emprestimoService = emprestimoService;
    }

    public IActionResult Index()
    {
        ViewBag.TotalLivros = _livroService.ListarTodos().Count;
        ViewBag.TotalAutores = _autorService.ListarTodos().Count;
        ViewBag.EmprestimosAtivos = _emprestimoService.ListarTodos().Count(e => !e.Devolvido);
        return View();
    }
}
