using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodShoppingCartMvcUI.Models
{
    [Table("Cuisines")]
    public class Cuisines
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string CuisineName { get; set; }
        public List<Food> Foods { get; set; }
    }
}
