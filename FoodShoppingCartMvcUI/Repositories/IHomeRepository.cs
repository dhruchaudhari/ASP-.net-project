namespace FoodShoppingCartMvcUI
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Food>> GetFoods(string sTerm = "", int CuisineId = 0);
        Task<IEnumerable<Cuisines>> Cuisines();
    }
}