using Libary.ViewModel.CardViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.CardServices;
using Service.OrderServices;
using Service.ViewModel.BookHistory;

namespace Libary.Controllers
{
	public class CardController : Controller
	{
		private readonly ICardService _cardService;
		private readonly IOrderService _orderService;
		public CardController (ICardService cardService, IOrderService orderService)
		{
			_cardService = cardService;
			_orderService = orderService;
		}

		[HttpPost("CreateOrder")]
		public async Task<IActionResult> CreateOrder(CardViewModels cardViewModels)
		{
			var studentSession = HttpContext.Session;
			string idStudent = studentSession.GetString("Student");
			if (!string.IsNullOrEmpty(idStudent) && !string.IsNullOrEmpty(cardViewModels.createOrderViewModel.studentCode) && !string.IsNullOrEmpty(cardViewModels.createOrderViewModel.CitizenId) && cardViewModels.createOrderViewModel.DatePay != null && !string.IsNullOrEmpty(cardViewModels.createOrderViewModel.PhoneNumber))
			{
				if (cardViewModels != null && cardViewModels.createOrderViewModel != null)
				{
					var isCreate = await _orderService.CreateOrderAsyn(idStudent, cardViewModels.createOrderViewModel.CitizenId, cardViewModels.createOrderViewModel.studentCode, cardViewModels.createOrderViewModel.DatePay, cardViewModels.createOrderViewModel.PhoneNumber);
					if (isCreate)
					{
						return RedirectToAction("Index", new { success = "Create Order Success" });
					}
					else
					{
						return RedirectToAction("Index", new { error = "Create Order Error" });
					}
				}
				else return RedirectToAction("Index", new { success = "Create Order Error" });
			}else return RedirectToAction("Login","Auth", new { success = "Login First" });
		}

		public async Task<IActionResult> Disable(string id)
		{
			var studentSession = HttpContext.Session;
			string idStudent = studentSession.GetString("Student");
			if (!string.IsNullOrEmpty(idStudent))
			{
				var isDisable = await _cardService.DisableAsync(id, idStudent);
				if (isDisable)
				{
					return RedirectToAction("Index", new { success = "Delete Success"});
				}
				else
				{
					return RedirectToAction("Index", new { error = "Delete Error" });
				}
			}
			else
			{
				return RedirectToAction("Login", "Auth", new { error = "Login first!" });
			}
		}

		public async Task<IActionResult> Index(string error, string success)
		{
			var studentSession = HttpContext.Session;
			string idStudent = studentSession.GetString("Student");
			if (!string.IsNullOrEmpty(idStudent))
			{
				var currentCard = await _cardService.GetCardByStudentAsync(idStudent);
				CardViewModels cardViewModels = new CardViewModels();
				if (currentCard != null)
				{

					ListBookHistoryViewModel listBookHistoryViewModel = new ListBookHistoryViewModel();
					foreach (var item in currentCard.Histories)
					{
						listBookHistoryViewModel.Books.Add(new BookViewModel
						{
							id = item.Id,
							name = item.Book.Name,
							ImgUrl = item.Book.ImgUrl,
							number = item.Number,
						});
					}
					cardViewModels.ListBookHistoryViewModel = listBookHistoryViewModel;
				}
				return View(cardViewModels);
			}
			else
			{
				return RedirectToAction("Login","Auth", new {error = "Login first!"});
			}
			
		}
	}
}
