using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OrderServices
{
	public interface IOrderService
	{
        Task<bool> CreateOrderAsyn(string studentId, string citizendId, string studentCode, DateTime datePay, string phone);
		Task<List<Order>> getAllOrderAsync();
		Task<List<Order>> getOrderByStudenIdAsync(string id);

        Task<bool> DisableAsync(string id);
		Task<bool> AcceptAsync(string id);
		Task<bool> PayAsync(string id);
    }
	public class OrderService: IOrderService
	{
		private readonly BookContext _context;
		public OrderService(BookContext bookContext) { 
			_context = bookContext;
		}

		public async Task<List<Order>> getAllOrderAsync()
		{
			return await _context.Orders.Where(p => p.IsActive).Include("Card").Include("Card.Histories").Include("Card.Student").ToListAsync();
		}
        public async Task<List<Order>> getOrderByStudenIdAsync(string id)
        {
			var result = new List<Order>();
            var listTmp = await _context.Orders.Where(p => p.IsActive).Include("Card").Include("Card.Histories").Include("Card.Student").ToListAsync();
			if (listTmp.Count > 0)
			{
				foreach (var item in listTmp)
				{
					if(item.Card.StudentId == id)
					{
						result.Add(item);
					}
				}
				return result;
			}
			else return null;
        }

        public async Task<bool> DisableAsync(string id)
		{
			var currentOrder = await _context.Orders.Where(p => p.Id == id && p.IsActive).FirstOrDefaultAsync();
			if (currentOrder != null)
			{
				currentOrder.IsActive = false;
				_context.Orders.Update(currentOrder);
				await _context.SaveChangesAsync();
				return true;
			}
			else return false;
		}

        public async Task<bool> AcceptAsync(string id)
        {
            var currentOrder = await _context.Orders.Where(p => p.Id == id && p.IsActive).FirstOrDefaultAsync();
            if (currentOrder != null)
            {
                currentOrder.IsAccept = true;
                _context.Orders.Update(currentOrder);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<bool> PayAsync(string id)
        {
            var currentOrder = await _context.Orders.Where(p => p.Id == id && p.IsActive).FirstOrDefaultAsync();
            if (currentOrder != null)
            {
                currentOrder.IsPay = true;
                _context.Orders.Update(currentOrder);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }    

        public async Task<bool> CreateOrderAsyn(string studentId, string citizendId, string studentCode, DateTime datePay, string phone)
		{
			var currentCard = await _context.Cards.Where(p => p.StudentId == studentId && p.IsActive).FirstOrDefaultAsync();
			if (currentCard != null)
			{
				var newOrder = new Order()
				{
					Id = Guid.NewGuid().ToString(),
					CardId = currentCard.Id,
					DateTime = DateTime.Now,
					IsAccept = false,
					IsActive = true,
					CitizenId = citizendId,
					StudentCode = studentCode,
					PhoneNum = phone,
					IsPay = false,
					DatePay = datePay
				};
				currentCard.IsActive = false;
				_context.Cards.Update(currentCard);
				Card newCard = new Card()
				{
					Id = Guid.NewGuid().ToString(),
					OrderId = Guid.NewGuid().ToString(),
					StudentId = studentId,
					IsActive = true,
				};
				await _context.Cards.AddAsync(newCard);
				await _context.Orders.AddAsync(newOrder);
				await _context.SaveChangesAsync();
				return true;
			}
			else return false;
		}
	}
}
