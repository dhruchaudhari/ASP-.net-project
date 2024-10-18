using FoodShoppingCartMvcUI.Models;
using FoodShoppingCartMvcUI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="",int cuisineId=0)
        {
            IEnumerable<Food> foods = await _homeRepository.GetFoods(sterm, cuisineId);
            IEnumerable<Cuisines> cuisines = await _homeRepository.Cuisines();
            FoodDisplayModel foodModel = new FoodDisplayModel
            {
              Foods=foods,
              Cuisines=cuisines,
              STerm=sterm,
              CuisineId=cuisineId
            };
            return View(foodModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}