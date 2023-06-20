using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserServices
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<bool> CreateUserAsync(string username, string password, string fullname);
        Task<bool> DisableEnableAsync(string id);
    }
    public class UserService:IUserService
    {
        private readonly BookContext _Context;
        public UserService(BookContext bookContext)
        {
            _Context = bookContext; 
        }

        public async Task<bool> DisableEnableAsync(string id)
        {
            var user = await _Context.Users.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.IsActive)
                {
                    user.IsActive = false;
                }
                else
                {
                    user.IsActive = true;

                }
                _Context.Users.Update(user);
                await _Context.SaveChangesAsync();  
                return true;
            }
            else return false;
        }
        public async Task<List<User>> GetAll()
        {
            return await _Context.Users.ToListAsync();
        }

        public async Task<bool> CreateUserAsync(string username, string password, string fullname)
        {
            var userCurrent = await _Context.Users.Where(p => p.Username == username && p.IsActive).FirstOrDefaultAsync();
            if (userCurrent == null)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Fullname = fullname,
                    Username = username,
                    Password = password,
                    IsActive = true,
                };
                await _Context.Users.AddAsync(newUser);
                await _Context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
    }
}
