using FoodShoppingCartMvcUI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodShoppingCartMvcUI.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class FoodController : Controller
{
    private readonly IFoodRepository _foodRepo;
    private readonly ICuisineRepository _cuisineRepo;
    private readonly IFileService _fileService;

    public FoodController(IFoodRepository foodRepo, ICuisineRepository cuisineRepo, IFileService fileService)
    {
        _foodRepo = foodRepo;
        _cuisineRepo = cuisineRepo;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index()
    {
        var foods = await _foodRepo.GetFoods();
        return View(foods);
    }

    public async Task<IActionResult> AddFood()
    {
        var cuisineSelectList = (await _cuisineRepo.GetCuisines()).Select(cuisine => new SelectListItem
        {
            Text = cuisine.CuisineName,
            Value = cuisine.Id.ToString(),
        });
        FoodDTO foodToAdd = new() { CuisineList = cuisineSelectList };
        return View(foodToAdd);
    }

    [HttpPost]
    public async Task<IActionResult> AddFood(FoodDTO foodToAdd)
    {
        var cuisineSelectList = (await _cuisineRepo.GetCuisines()).Select(cuisine => new SelectListItem
        {
            Text = cuisine.CuisineName,
            Value = cuisine.Id.ToString(),
        });
        foodToAdd.CuisineList = cuisineSelectList;

        if (!ModelState.IsValid)
            return View(foodToAdd);

        try
        {
            if (foodToAdd.ImageFile != null)
            {
                if(foodToAdd.ImageFile.Length> 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg",".jpg",".png"];
                string imageName=await _fileService.SaveFile(foodToAdd.ImageFile, allowedExtensions);
                foodToAdd.Image = imageName;
            }
            // manual mapping of FoodDTO -> Food
            Food food = new()
            {
                Id = foodToAdd.Id,
                FoodName = foodToAdd.FoodName,
                RestorantName = foodToAdd.RestorantName,
                Image = foodToAdd.Image,
                CuisineId = foodToAdd.CuisineId,
                Price = foodToAdd.Price
            };
            await _foodRepo.AddFood(food);
            TempData["successMessage"] = "Food is added successfully";
            return RedirectToAction(nameof(AddFood));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"]= ex.Message;
            return View(foodToAdd);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(foodToAdd);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(foodToAdd);
        }
    }

    public async Task<IActionResult> UpdateFood(int id)
    {
        var food = await _foodRepo.GetFoodById(id);
        if(food==null)
        {
            TempData["errorMessage"] = $"Food with the id: {id} does not found";
            return RedirectToAction(nameof(Index));
        }
        var cuisineSelectList = (await _cuisineRepo.GetCuisines()).Select(cuisine => new SelectListItem
        {
            Text = cuisine.CuisineName,
            Value = cuisine.Id.ToString(),
            Selected=cuisine.Id==food.CuisineId
        });
        FoodDTO foodToUpdate = new() 
        { 
            CuisineList = cuisineSelectList,
            FoodName=food.FoodName,
            RestorantName=food.RestorantName,
            CuisineId=food.CuisineId,
            Price=food.Price,
            Image=food.Image 
        };
        return View(foodToUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateFood(FoodDTO foodToUpdate)
    {
        var cuisineSelectList = (await _cuisineRepo.GetCuisines()).Select(cuisine => new SelectListItem
        {
            Text = cuisine.CuisineName,
            Value = cuisine.Id.ToString(),
            Selected=cuisine.Id==foodToUpdate.CuisineId
        });
        foodToUpdate.CuisineList = cuisineSelectList;

        if (!ModelState.IsValid)
            return View(foodToUpdate);

        try
        {
            string oldImage = "";
            if (foodToUpdate.ImageFile != null)
            {
                if (foodToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                string imageName = await _fileService.SaveFile(foodToUpdate.ImageFile, allowedExtensions);
                // hold the old image name. Because we will delete this image after updating the new
                oldImage = foodToUpdate.Image;
                foodToUpdate.Image = imageName;
            }
            // manual mapping of FoodDTO -> Food
            Food food = new()
            {
                Id=foodToUpdate.Id,
                FoodName = foodToUpdate.FoodName,
                RestorantName = foodToUpdate.RestorantName,
                CuisineId = foodToUpdate.CuisineId,
                Price = foodToUpdate.Price,
                Image = foodToUpdate.Image
            };
            await _foodRepo.UpdateFood(food);
            // if image is updated, then delete it from the folder too
            if(!string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
            TempData["successMessage"] = "Food is updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(foodToUpdate);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(foodToUpdate);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(foodToUpdate);
        }
    }

    public async Task<IActionResult> DeleteFood(int id)
    {
        try
        {
            var food = await _foodRepo.GetFoodById(id);
            if (food == null)
            {
                TempData["errorMessage"] = $"Food with the id: {id} does not found";
            }
            else
            {
                await _foodRepo.DeleteFood(food);
                if (!string.IsNullOrWhiteSpace(food.Image))
                {
                    _fileService.DeleteFile(food.Image);
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on deleting the data";
        }
        return RedirectToAction(nameof(Index));
    }

}
