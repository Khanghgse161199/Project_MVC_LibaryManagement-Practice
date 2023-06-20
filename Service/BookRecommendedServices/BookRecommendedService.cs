using DataService.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BookRecommendedServices
{
	public interface IBookRecommendedService
	{

	}
	public class BookRecommendedService:IBookRecommendedService
	{
		private readonly BookContext _context;
		public BookRecommendedService(BookContext context)
		{
			_context = context;
		}

		//public async Task<bool> addToCardAsyn(string id)
		//{

		//}
	}
}
