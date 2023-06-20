using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AuthService
{
    public interface IAuthService
    {
        Task<string> LoginUser(string username, string password);
        Task<string> LoginStudent(string email, string password);

	}
    public class AuthService:IAuthService
    {
        private readonly BookContext _context;
        public AuthService (BookContext bookContext)
        {
            _context = bookContext;
        }

        public async Task<string> LoginUser(string username, string password)
        {
            var loginUser = await _context.Users.Where(p => p.Username == username && p.Password == p.Password && p.IsActive).FirstOrDefaultAsync();
            if (loginUser != null)
            {
                return loginUser.Id;
            }else return string.Empty;
        }

        public async Task<string> LoginStudent(string email, string password)
        {
            var loginStudent = await _context.Students.Where(p => p.Email == email && p.Password == password && p.IsActive).FirstOrDefaultAsync();
            if (loginStudent != null)
            {
                return loginStudent.Id;
            }
            else return string.Empty;
        }
    }
}
