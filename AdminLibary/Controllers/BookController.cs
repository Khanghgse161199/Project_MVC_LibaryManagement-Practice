using AdminLibary.ViewModel.Book;
using AdminLibary.ViewModel.BookViewModel;
using AdminLibary.ViewModel.CategoryViewmodel;
using Microsoft.AspNetCore.Mvc;
using Service.BookServices;
using Service.CategoryServices;

namespace AdminLibary.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        public BookController(IBookService bookService, ICategoryService categoryService) { 
            _bookService = bookService;
            _categoryService = categoryService;
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBookViewModel createBookViewModel)
        {
            if (ModelState.IsValid)
            {
                var seesion = HttpContext.Session;
                string userId = seesion.GetString("USER");
                if (!string.IsNullOrEmpty(userId))
                {
                    var isCreate = await _bookService.CreateBookAsync(createBookViewModel.info, userId, createBookViewModel.info.CategoryId);
                    if (isCreate)
                    {
                        return RedirectToAction("Create", new { Success = "Create book success"});
                    }
                    else
                    {
                        return RedirectToAction("Create", new { Success = "Create book Error" });
                    }
                }
                else return RedirectToAction("Index", new { Success = "Create book Error" });
            }
            else
            {
                var error = ModelState.Select(p => p.Value.Errors)
                    .Where(p => p.Count > 0)
                    .ToList();
                string stringError = "";
                foreach (var item in error)
                {
                    stringError += item.ToString() + " ";
                }
                return RedirectToAction("Index", "Home", new { error = stringError });
            }
        }
        public async Task<IActionResult> enable(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var isEnable = await _bookService.EnableDisableAsync(id);
                if (isEnable)
                {
                    return RedirectToAction("Index", new { success = "Enable book is success" });
                }
                else return RedirectToAction("Index", new { error = "Enable book is error" });
            }
            else return RedirectToAction("Index", new { error = "Enable book is error" });
        }

        public async Task<IActionResult> disable(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var isEnable = await _bookService.EnableDisableAsync(id);
                if (isEnable)
                {
                    return RedirectToAction("Index", new { success = "Disable book is success" });
                }
                else return RedirectToAction("Index", new { error = "Disable book is error" });
            }
            else return RedirectToAction("Index", new { error = "Disable book is error" });
        }

        public async Task<IActionResult> Index(string error, string success)
        {
            var books = await _bookService.GetAllAsync();
            ListBookViewModel listBookViewModel = new ListBookViewModel();
            if (books.Count > 0)
            {              
                foreach (var item in books)
                {
                    listBookViewModel.Books.Add(new BookViewModel
                    {
                        Id = item.Id,
                        //Creator = item.Creator,
                        Name = item.Name,
                        ImgUrl = item.ImgUrl,
                        Author = item.Author,
                        PulishDate = item.PulishDate,
                        Pulisher = item.Pulisher,
                        CategoryName = item.Category.Name,
                        isActive = item.IsActive,
                    });
                }             
            }
            else
            {
                ViewBag.error = "null book";               
            }

            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
            }
            if (!string.IsNullOrEmpty(success))
            {
                ViewBag.Success = success;
            }
            return View(listBookViewModel);
        }

        public async Task<IActionResult> Create(string error, string success)
        {
            var categorys = await _categoryService.GetAvailableAsync();
            CreateBookViewModel createBookViewModel = new CreateBookViewModel();
            if (categorys != null && categorys.Count > 0)
            {
                createBookViewModel.categories = categorys;
                if (!string.IsNullOrEmpty(error))
                {
                    ViewBag.Error = error;
                }
                if (!string.IsNullOrEmpty(success))
                {
                    ViewBag.Success = success;
                }
                return View(createBookViewModel);
            }
            else
            {
                return RedirectToAction("Index","Home", new { error = "Cant not found category"});
            }
            
          
        }
    }
}
