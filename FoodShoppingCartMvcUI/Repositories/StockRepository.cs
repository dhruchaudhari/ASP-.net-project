using Microsoft.EntityFrameworkCore;

namespace FoodShoppingCartMvcUI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByFoodId(int foodId) => await _context.Stocks.FirstOrDefaultAsync(s => s.FoodId == foodId);

        //public async Task ManageStock(StockDTO stockToManage)
        //{
        //    // if there is no stock for given food id, then add new record
        //    // if there is already stock for given food id, update stock's quantity
        //    var existingStock = await GetStockByFoodId(stockToManage.FoodId);
        //    if (existingStock is null)
        //    {
        //        var stock = new Stock { FoodId = stockToManage.FoodId, Quantity = stockToManage.Quantity };
        //        _context.Stocks.Add(stock);
        //    }
        //    else
        //    {
        //        existingStock.Quantity = stockToManage.Quantity;
        //    }
        //    await _context.SaveChangesAsync();
        //}


        //public async Task ManageStock(StockDTO stockToManage)
        //{
        //    var existingStock = await GetStockByFoodId(stockToManage.FoodId);
        //    if (existingStock is null)
        //    {
        //        // Create new stock if none exists
        //        var stock = new Stock
        //        {
        //            FoodId = stockToManage.FoodId,
        //            Quantity = stockToManage.Quantity
        //        };
        //        _context.Stocks.Add(stock);
        //        Console.Write("eif");
        //    }
        //    else
        //    {
        //        // Update stock quantity if it exists
        //        Console.Write("else");
        //        existingStock.Quantity = stockToManage.Quantity;
        //        _context.Stocks.Update(existingStock);  // Ensure you're updating the entity
        //    }

        //    // Save changes
        //    await _context.SaveChangesAsync();
        //}

        public async Task ManageStock(StockDTO stockToManage)
        {
            // Check if the FoodId exists in the Food table
            var foodExists = await _context.Foods.AnyAsync(f => f.Id == stockToManage.FoodId);

            if (!foodExists)
            {
                throw new InvalidOperationException("FoodId does not exist in the Food table.");
            }

            var existingStock = await GetStockByFoodId(stockToManage.FoodId);
            if (existingStock is null)
            {
                // Create new stock if none exists
                var stock = new Stock
                {
                    FoodId = stockToManage.FoodId,
                    Quantity = stockToManage.Quantity
                };
                _context.Stocks.Add(stock);
            }
            else
            {
                // Update stock quantity if it exists
                existingStock.Quantity = stockToManage.Quantity;
                _context.Stocks.Update(existingStock);
            }

            // Save changes
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from food in _context.Foods
                                join stock in _context.Stocks
                                on food.Id equals stock.FoodId
                                into food_stock
                                from foodStock in food_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || food.FoodName.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    FoodId = food.Id,
                                    FoodName = food.FoodName,
                                    Quantity = foodStock == null ? 0 : foodStock.Quantity
                                }
                                ).ToListAsync();
            return stocks;
        }

    }

    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByFoodId(int foodId);
        Task ManageStock(StockDTO stockToManage);
    }
}
