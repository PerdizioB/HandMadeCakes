using HandMadeCakes.Dto;
using HandMadeCakes.Models;
using HandMadeCakes.Services.Cake;
using Microsoft.AspNetCore.Mvc;

public class CakeController : Controller
{
    private readonly ICakeInterface _cakeInterface;

    public CakeController(ICakeInterface cakeInterface)
    {
        _cakeInterface = cakeInterface;
    }

    public async Task<IActionResult> Index()
    {
        var cakes = await _cakeInterface.GetCakes();

        return View(cakes);
    }

    // GET para abrir o formulário de registro
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST para salvar o novo bolo
    [HttpPost]
    public async Task<IActionResult> Register(CakeCreateDto cakeCreateDto, IFormFile foto, List<IFormFile>? fotos)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var cake = await _cakeInterface.CriarCake(cakeCreateDto, foto, fotos);
                return RedirectToAction("Index", "Cake");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error registering cake: " + ex.Message);
            }
        }
        return View(cakeCreateDto);
    }

    public async Task<IActionResult> Details(int id)
    {
        var cake = await _cakeInterface.GetCakePorId(id);
        return View(cake);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cake = await _cakeInterface.GetCakePorId(id);
        return View(cake);
    }

    public async Task<IActionResult> Remover(int id)
    {
        var cake = await _cakeInterface.RemoverCake(id);
        return RedirectToAction("Index", "Cake");
    }


    [HttpPost]
    public async Task<IActionResult> Edit(CakeModel cakeModel, IFormFile? foto, List<IFormFile>? fotos)
    {
        ModelState.Remove("Category");

        if (ModelState.IsValid)
        {
            try
            {
                var cake = await _cakeInterface.Edit(cakeModel, foto, fotos);
                return RedirectToAction("Index", "Cake");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error editing cake: " + ex.Message);
            }
        }

        // Recarrega as imagens para não quebrar a view
        var cakeWithImages = await _cakeInterface.GetCakePorId(cakeModel.Id);
        return View(cakeWithImages);
    }

}
