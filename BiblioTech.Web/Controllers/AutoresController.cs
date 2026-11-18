using BiblioTech.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Web.Controllers;

public class AutoresController : Controller
{
    private readonly AutorService _autorService;

    public AutoresController(AutorService autorService)
    {
        _autorService = autorService;
    }

    public IActionResult Index()
    {
        var autores = _autorService.ListarTodos();
        return View(autores);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(string nome, string nacionalidade)
    {
        try
        {
            _autorService.CadastrarAutor(nome, nacionalidade);
            TempData["Sucesso"] = "Autor cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            return View();
        }
    }
}
