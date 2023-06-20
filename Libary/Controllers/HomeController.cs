using Libary.ViewModel.BookRecommend;
using Microsoft.AspNetCore.Mvc;
using Service.BookServices;
using Service.HistoryServices;

namespace Libary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IHistoryService _historyService;
        public HomeController(IBookService bookService, IHistoryService historyService)
		{
			_bookService = bookService;
            _historyService = historyService;
		}

       
        public async Task<IActionResult> CreateHistory(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var studentSession = HttpContext.Session;
                string studentId = studentSession.GetString("Student");
                if (!string.IsNullOrEmpty(studentId))
                {
                    var isCreate = await _historyService.CreateHistoryAsync(id, studentId);
                    if (isCreate)
                    {
						return RedirectToAction("Index", new { success = "Add to card success." });
					}
					else return RedirectToAction("Index", new { error = "This book exist in card!" });
				}
				else return RedirectToAction("Index", new { error = "Error when quick-add!" });
			}
			else return RedirectToAction("Index", new { error = "Error when quick-add!"});
        }

		public async Task<IActionResult> Index(string error, string success)
        {
            var books = await _bookService.GetAllAsync();
			ListBookRecommended listBookRecommended = new ListBookRecommended();
            if(books.Count > 0)
            {
                foreach (var book in books)
                {
                    listBookRecommended.books.Add(new BookRecommendViewModel
                    {
                        Id = book.Id,
                        Name = book.Name,
                        ImgUrl = book.ImgUrl,
                        Author = book.Author,
                        Pulisher = book.Pulisher,
                        PulishDate = book.PulishDate,
                        CategoryName = book.Category.Name,
                    });
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.Error = error;
            }
            if (!string.IsNullOrEmpty(success))
            {
                ViewBag.Success = success;
            }

            return View(listBookRecommended);
        }
    }
}
