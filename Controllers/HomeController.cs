using HandMadeCakes.Services.Cake;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HandMadeCakes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICakeInterface _CakeInterface;

        public HomeController(ICakeInterface CakeInterface)
        {
            _CakeInterface = CakeInterface;
        }
        public async Task<IActionResult> Index(string? pesquisar)
        {
            if (pesquisar == null)
            {
                var cakes = await _CakeInterface.GetCakes();
                return View(cakes);
            }
            else
            {
                var cakes = await _CakeInterface.GetCakesFiltro(pesquisar);
                return View(cakes);
            }

        }

    }
}