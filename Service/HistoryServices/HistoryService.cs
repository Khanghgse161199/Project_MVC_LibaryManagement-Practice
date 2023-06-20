using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HistoryServices
{
	public interface IHistoryService
	{
		Task<bool> CreateHistoryAsync(string id, string carId);
		Task<List<History>> GetAllHistoyByCardId(string id);

    }
	public class HistoryService:IHistoryService
	{
		private readonly BookContext _context;
		public HistoryService(BookContext bookContext) {
			_context = bookContext;
		}
		public async Task<bool> CreateHistoryAsync(string id, string studentId)
		{
			var currentCard = await _context.Cards.Where(p => p.StudentId == studentId && p.IsActive).Include("Histories").FirstOrDefaultAsync();
			if (currentCard != null)
			{
				foreach (var item in currentCard.Histories)
				{
					if(item.BookId == id)
					{
						return false;
					}
				}
				var currentBook = await _context.Books.Where(p => p.Id == id && p.IsActive).Include("Category").FirstOrDefaultAsync();
				if (currentBook != null)
				{
					var newHistory = new History()
					{
						Id = Guid.NewGuid().ToString(),
						CardId = currentCard.Id,
						DateTime = DateTime.Now,
						Number = 1,
						BookId = id
					};

					await _context.Histories.AddAsync(newHistory);
					await _context.SaveChangesAsync();
					return true;
				}
				else return false;
			} else return false;
		}

        public async Task<List<History>> GetAllHistoyByCardId(string id)
        {
            var Histories = await _context.Histories.Where(p => p.CardId == id).Include("Book").Include("Book.Category").ToListAsync();
            if (Histories != null)
            {           
                return Histories;
            }
            else return null;
        }
    }
}
