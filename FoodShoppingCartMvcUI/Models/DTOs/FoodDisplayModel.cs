namespace FoodShoppingCartMvcUI.Models.DTOs
{
    public class FoodDisplayModel
    {
        public IEnumerable<Food> Foods { get; set; }
        public IEnumerable<Cuisines> Cuisines { get; set; }
        public string STerm { get; set; } = "";
        public int CuisineId { get; set; } = 0;
    }
}
