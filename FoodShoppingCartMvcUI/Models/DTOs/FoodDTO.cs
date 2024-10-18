using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FoodShoppingCartMvcUI.Models.DTOs;
public class FoodDTO
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
    public IFormFile? ImageFile { get; set; }
    public IEnumerable<SelectListItem>? CuisineList { get; set; }
}
