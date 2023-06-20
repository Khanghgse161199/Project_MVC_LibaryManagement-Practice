using Libary.ViewModel.OrderViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.HistoryServices;
using Service.OrderServices;

namespace Libary.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IOrderService _orderService;
        public OrderController(IHistoryService historyService, IOrderService orderService)
        {
			_historyService = historyService;
			_orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
			var currentStudent = HttpContext.Session;
			string studentId = currentStudent.GetString("Student");
			var orders = await _orderService.getOrderByStudenIdAsync(studentId);
			ListOrderViewModel listOrderViewModel = new ListOrderViewModel();
			if (orders != null && orders.Count > 0)
			{
				foreach (var order in orders)
				{
					listOrderViewModel.orderViewModels.Add(new OrderViewModel
					{
						Id = order.Id,
						CardId = order.CardId,
						DateTime = order.DateTime,
						DatePay = order.DatePay,
						IsAccept = order.IsAccept,
						IsActive = order.IsActive,
						CitizenId = order.CitizenId,
						StudentCode = order.StudentCode,
						IsPay = order.IsPay,
						PhoneNum = order.PhoneNum,
						Card = order.Card,
						Historys = await _historyService.GetAllHistoyByCardId(order.CardId)
					});
				}
			}
			else
			{
				return RedirectToAction("Index","Home", new { error = "no order..." });
			}
			return View(listOrderViewModel);
        }
    }
}
