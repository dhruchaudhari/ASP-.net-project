using Microsoft.EntityFrameworkCore;

namespace FoodShoppingCartMvcUI.Repositories;

public interface ICuisineRepository
{
    Task AddCuisine(Cuisines cuisine);
    Task UpdateCuisine(Cuisines cuisine);
    Task<Cuisines?> GetCuisineById(int id);
    Task DeleteCuisine(Cuisines cuisine);
    Task<IEnumerable<Cuisines>> GetCuisines();
}
public class CuisineRepository : ICuisineRepository
{
    private readonly ApplicationDbContext _context;
    public CuisineRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCuisine(Cuisines cuisine)
    {
        _context.Cuisines.Add(cuisine);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCuisine(Cuisines cuisine)
    {
        _context.Cuisines.Update(cuisine);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCuisine(Cuisines cuisine)
    {
        _context.Cuisines.Remove(cuisine);
        await _context.SaveChangesAsync();
    }

    public async Task<Cuisines?> GetCuisineById(int id)
    {
        return await _context.Cuisines.FindAsync(id);
    }

    public async Task<IEnumerable<Cuisines>> GetCuisines()
    {
        return await _context.Cuisines.ToListAsync();
    }

    
}
