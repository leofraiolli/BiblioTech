using BiblioTech.Web.Models;
using BiblioTech.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Web.Controllers;

public class LivrosController : Controller
{
    private readonly LivroService _livroService;
    private readonly AutorService _autorService;

    public LivrosController(LivroService livroService, AutorService autorService)
    {
        _livroService = livroService;
        _autorService = autorService;
    }

    public IActionResult Index()
    {
        var livros = _livroService.ListarTodos();
        return View(livros);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Autores = _autorService.ListarTodos();
        return View(new Livro { AnoPublicacao = DateTime.Now.Year, QuantidadeDisponivel = 1 });
    }

    [HttpPost]
    public IActionResult Create(Livro livro, int[] autoresIds)
    {
        try
        {
            _livroService.CadastrarLivro(livro, autoresIds);
            TempData["Sucesso"] = "Livro cadastrado e vinculado aos autores com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            ViewBag.Autores = _autorService.ListarTodos();
            return View(livro);
        }
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var livro = _livroService.BuscarPorId(id);
        if (livro is null) return NotFound();

        ViewBag.Autores = _autorService.ListarTodos();
        return View(livro);
    }

    [HttpPost]
    public IActionResult Edit(Livro livro, int[] autoresIds)
    {
        try
        {
            _livroService.AtualizarLivro(livro, autoresIds);
            TempData["Sucesso"] = "Livro atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            ViewBag.Autores = _autorService.ListarTodos();
            return View(livro);
        }
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        try
        {
            _livroService.ExcluirLivro(id);
            TempData["Sucesso"] = "Livro removido do acervo.";
        }
        catch (Exception ex)
        {
            TempData["Erro"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
