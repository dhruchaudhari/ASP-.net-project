using System.ComponentModel.DataAnnotations;

namespace FoodShoppingCartMvcUI.Models.DTOs
{
    public class StockDTO
    {
        public int FoodId { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }
    }
}
