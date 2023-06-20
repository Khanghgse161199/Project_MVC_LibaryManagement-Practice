using AdminLibary.ViewModel.CategoryViewmodel;
using Microsoft.AspNetCore.Mvc;
using Service.CategoryServices;

namespace AdminLibary.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel categoryViewModel)
        {
            if (categoryViewModel != null)
            {
                var isCreate = await _categoryService.CreateAsync(categoryViewModel.nameCategory);
                if (isCreate)
                {
                    return RedirectToAction("Create", new { success = "Create Category success" });
                }
                else
                {
                    return RedirectToAction("Create", new { success = "Create Category error" });
                }
            }
            else
            {
                return RedirectToAction("Index","User", new { success = "Create Category error" });
            }
        }

        public async Task<IActionResult> disable(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var isDisable = await _categoryService.DisableOrEnableAsync(id);
                if (isDisable)
                {
                    return RedirectToAction("Index", new { success = "Dissable Category success" });
                }
                else
                {
                    return RedirectToAction("Index", new { error = "Dissable Category error" });
                }
            }
            else{
                return RedirectToAction("Index", new { success = "Dissable Category error" });
            }
        }
        public async Task<IActionResult> enable(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var isDisable = await _categoryService.DisableOrEnableAsync(id);
                if (isDisable)
                {
                    return RedirectToAction("Index", new { success = "Enable Category success" });
                }
                else
                {
                    return RedirectToAction("Index", new { error = "Enable Category error" });
                }
            }
            else
            {
                return RedirectToAction("Index", new { success = "Enable Category error" });
            }
        }

        public async Task<IActionResult> Index(string error, string success)
        {
            var categorys = await _categoryService.GetAllCategoryAsync();
            ListCategoryViewModel listCategoryViewModel = new ListCategoryViewModel();
            if(categorys.Count > 0)
            {
                foreach(var category in categorys) {
                    listCategoryViewModel.categorys.Add(new CategoryViewModel
                    {
                        id = category.Id,
                        name = category.Name,
                        isActive = category.IsActive,
                    });
                }
            }
            else
            {
                ViewBag.error = "null category";
            }
            if (error != null)
            {
                ViewBag.Error = error;
            }
            if (success != null)
            {
                ViewBag.Success = success;
            }
            return View(listCategoryViewModel);
        }

        public async Task<IActionResult> Create(string error, string success)
        {
            var categorys = await _categoryService.GetAllCategoryAsync();
            if (error != null)
            {
                ViewBag.Error = error;
            }
            if (success != null)
            {
                ViewBag.Success = success;
            }
            return View();
        }
    }
}
