using System.ComponentModel.DataAnnotations;

namespace FoodShoppingCartMvcUI.Models.DTOs
{
    public class CuisineDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string CuisineName { get; set; }
    }
}
