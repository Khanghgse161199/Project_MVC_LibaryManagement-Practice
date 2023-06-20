using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CardServices
{
	public interface ICardService
	{
		Task<Card> GetCardByStudentAsync(string Id);
		Task<bool> DisableAsync(string id, string StudentId);
	}
	public class CardService: ICardService
	{
		private readonly BookContext _context;
		public CardService(BookContext bookContext) { 
			_context = bookContext;
		}

		public async Task<Card> GetCardByStudentAsync(string Id)
		{
			var currentCard = await _context.Cards.Where(p => p.StudentId == Id && p.IsActive).Include("Histories").Include("Histories.Book").FirstOrDefaultAsync();
			if (currentCard != null)
			{
				return currentCard;
			}
			else return null;
		}

		public async Task<bool> DisableAsync(string id, string StudentId)
		{
			var currentCard = await _context.Cards.Where(p => p.StudentId == StudentId && p.IsActive).Include("Histories").FirstOrDefaultAsync();
			if(currentCard != null)
			{
				var currnetHistory = await _context.Histories.Where(p => p.Id == id).FirstOrDefaultAsync();
				if (currnetHistory != null && currentCard.Histories.Contains(currnetHistory))
				{
					_context.Histories.Remove(currnetHistory);
					await _context.SaveChangesAsync();
					return true;
				}else return false;				
			}else return false;
		}
	}
}
