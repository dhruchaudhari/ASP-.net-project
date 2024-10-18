

using Microsoft.EntityFrameworkCore;

namespace FoodShoppingCartMvcUI.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Cuisines>> Cuisines()
        {
            return await _db.Cuisines.ToListAsync();
        }
        public async Task<IEnumerable<Food>> GetFoods(string sTerm = "", int cuisineId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Food> foods = await (from food in _db.Foods
                         join cuisine in _db.Cuisines
                         on food.CuisineId equals cuisine.Id
                         join stock in _db.Stocks
                         on food.Id equals stock.FoodId
                         into food_stocks
                         from foodWithStock in food_stocks.DefaultIfEmpty()
                         where string.IsNullOrWhiteSpace(sTerm) || (food != null && food.FoodName.ToLower().StartsWith(sTerm))
                         select new Food
                         {
                             Id = food.Id,
                             Image = food.Image,
                             RestorantName = food.RestorantName,
                             FoodName = food.FoodName,
                             CuisineId = food.CuisineId,
                             Price = food.Price,
                             CuisineName = cuisine.CuisineName,
                             Quantity=foodWithStock==null? 0:foodWithStock.Quantity
                         }
                         ).ToListAsync();
            if (cuisineId > 0)
            {

                foods = foods.Where(a => a.CuisineId == cuisineId).ToList();
            }
            return foods;

        }
    }
}
