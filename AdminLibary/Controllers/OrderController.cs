using AdminLibary.ViewModel.OrderViewModelAdmin;
using Microsoft.AspNetCore.Mvc;
using Service.HistoryServices;
using Service.OrderServices;

namespace AdminLibary.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IHistoryService _historyService;
		public OrderController(IOrderService orderService, IHistoryService historyService)
		{
			_orderService = orderService;
			_historyService = historyService;
		}

		public async Task<IActionResult> Accept(string id)
		{
			if (!string.IsNullOrEmpty(id))
			{
				var isAccept = await _orderService.AcceptAsync(id);
				if (isAccept)
				{
                    return RedirectToAction("Index", new { error = "Accept Success" });
                }else return RedirectToAction("Index", new { error = "error in accept" });
            }
            else return RedirectToAction("Index", new { error = "error in accept"});
		}

        public async Task<IActionResult> Finish(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var isAccept = await _orderService.PayAsync(id);
                if (isAccept)
                {
                    return RedirectToAction("Index", new { error = "Paied Success" });
                }
                else return RedirectToAction("Index", new { error = "error in accept" });
            }
            else return RedirectToAction("Index", new { error = "error in accept" });
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var isAccept = await _orderService.DisableAsync(id);
                if (isAccept)
                {
                    return RedirectToAction("Index", new { error = "Deleted Success" });
                }
                else return RedirectToAction("Index", new { error = "error in accept" });
            }
            else return RedirectToAction("Index", new { error = "error in accept" });
        }

        public async Task<IActionResult> Index(string error, string success)
		{
			var orders = await _orderService.getAllOrderAsync();
            ListOrderViewModelAdmin listOrderViewModelAdmin = new ListOrderViewModelAdmin();
            if (orders.Count > 0)
			{		
				foreach(var order in orders)
				{
					listOrderViewModelAdmin.orderViewModelAdmins.Add(new OrderViewModelAdmin
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
			return View(listOrderViewModelAdmin);
		}
	}
}
