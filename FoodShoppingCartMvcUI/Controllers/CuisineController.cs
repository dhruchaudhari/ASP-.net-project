using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodShoppingCartMvcUI.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CuisineController : Controller
    {
        private readonly ICuisineRepository _cuisineRepo;

        public CuisineController(ICuisineRepository cuisineRepo)
        {
            _cuisineRepo = cuisineRepo;
        }

        public async Task<IActionResult> Index()
        {
            var cuisines = await _cuisineRepo.GetCuisines();
            return View(cuisines);
        }

        public IActionResult AddCuisine()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCuisine(CuisineDTO cuisine)
        {
            if(!ModelState.IsValid)
            {
                return View(cuisine);
            }
            try
            {
                var cuisineToAdd = new Cuisines { CuisineName = cuisine.CuisineName, Id = cuisine.Id };
                await _cuisineRepo.AddCuisine(cuisineToAdd);
                TempData["successMessage"] = "Cuisine added successfully";
                return RedirectToAction(nameof(AddCuisine));
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = "Cuisine could not added!";
                return View(cuisine);
            }

        }

        public async Task<IActionResult> UpdateCuisine(int id)
        {
            var cuisine = await _cuisineRepo.GetCuisineById(id);
            if (cuisine is null)
                throw new InvalidOperationException($"Cuisine with id: {id} does not found");
            var cuisineToUpdate = new CuisineDTO
            {
                Id = cuisine.Id,
                CuisineName = cuisine.CuisineName
            };
            return View(cuisineToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCuisine(CuisineDTO cuisineToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(cuisineToUpdate);
            }
            try
            {
                var cuisine = new Cuisines { CuisineName = cuisineToUpdate.CuisineName, Id = cuisineToUpdate.Id };
                await _cuisineRepo.UpdateCuisine(cuisine);
                TempData["successMessage"] = "Cuisine is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Cuisine could not updated!";
                return View(cuisineToUpdate);
            }

        }

        public async Task<IActionResult> DeleteCuisine(int id)
        {
            var cuisine = await _cuisineRepo.GetCuisineById(id);
            if (cuisine is null)
                throw new InvalidOperationException($"Cuisine with id: {id} does not found");
            await _cuisineRepo.DeleteCuisine(cuisine);
            return RedirectToAction(nameof(Index));

        }

    }
}
