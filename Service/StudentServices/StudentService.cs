using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.StudentServices
{
	public interface IStudentService
	{
		Task<bool> CreateStudentAsync(string username, string password, string email);
	}
	public class StudentService:IStudentService
	{
		private readonly BookContext _context;
		public StudentService (BookContext bookContext)
		{
			_context = bookContext;
		}

		public async Task<bool> CreateStudentAsync(string username, string password, string email)
		{
			var currentStudent = await _context.Students.Where(p => p.Email == email && p.IsActive).FirstOrDefaultAsync();
			if (currentStudent == null) {
				string studentId = Guid.NewGuid().ToString();
				Student newStudent = new Student()
				{
					Id = studentId,
					Username = username,
					Password = password,
					Email = email,					
					IsActive = true
				};

				Card newCard = new Card()
				{
					Id = Guid.NewGuid().ToString(),
					OrderId = Guid.NewGuid().ToString(),
					StudentId = studentId,
					IsActive = true,
				};
				await _context.Students.AddAsync(newStudent);
				await _context.Cards.AddAsync(newCard);			
				await _context.SaveChangesAsync();
				return true;
			}
            else
            {
				return false;
            }
        }
	}
}
