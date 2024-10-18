using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodShoppingCartMvcUI.Models
{
    [Table("Food")]
    public class Food
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? FoodName { get; set; }

        [Required]
        [MaxLength(40)]
        public string? RestorantName { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int CuisineId { get; set; }
        public Cuisines Cuisine { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }

        [NotMapped]
        public string CuisineName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }


    }
}
