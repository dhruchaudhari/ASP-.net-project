using Microsoft.EntityFrameworkCore;

namespace FoodShoppingCartMvcUI.Repositories
{
    public interface IFoodRepository
    {
        Task AddFood(Food food);
        Task DeleteFood(Food food);
        Task<Food?> GetFoodById(int id);
        Task<IEnumerable<Food>> GetFoods();
        Task UpdateFood(Food food);
    }

    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext _context;
        public FoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFood(Food food)
        {
            _context.Foods.Update(food);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFood(Food food)
        {
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
        }

        public async Task<Food?> GetFoodById(int id) => await _context.Foods.FindAsync(id);

        public async Task<IEnumerable<Food>> GetFoods() => await _context.Foods.Include(a=>a.Cuisine).ToListAsync();
    }
}
