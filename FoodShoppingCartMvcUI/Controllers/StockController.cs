using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodShoppingCartMvcUI.Controllers
{
    [Authorize(Roles=nameof(Roles.Admin))]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepo;
       

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            

        }

        public async Task<IActionResult> Index(string sTerm="")
        {
            var stocks=await _stockRepo.GetStocks(sTerm);
            return View(stocks);
        }

        public async Task<IActionResult> ManangeStock(int foodId)
        {
            var existingStock = await _stockRepo.GetStockByFoodId(foodId);
            var stock = new StockDTO
            {
                FoodId = foodId,
                Quantity = existingStock != null
            ? existingStock.Quantity : 0
            };
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> ManageStock(StockDTO stock)
        {

            await _stockRepo.ManageStock(stock);

            //TempData["successMessage"] = "Stock is updated successfully.";

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public async Task<IActionResult> ManangeStock(StockDTO stock)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("if");
                    await _stockRepo.ManageStock(stock);

                    TempData["successMessage"] = "Stock is updated successfully.";
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = "Something went wrong!!";  // Log or inspect the exception
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("else");
                return View(stock);
            }
        }

    }
}
