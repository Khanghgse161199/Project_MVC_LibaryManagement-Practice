using DataService.Entities;
using Microsoft.EntityFrameworkCore;
using Service.ViewModel.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BookServices
{
    public interface IBookService
    {
        Task<bool> CreateBookAsync(CreateBookViewModel info, string creator, string categoryId);
        Task<bool> EnableDisableAsync(string id);
        Task<List<Book>> GetAllAsync();
    }
    public class BookService: IBookService
    {
        private readonly BookContext _context;
        public BookService (BookContext bookContext)
        {
            _context = bookContext;
        }

        public async Task<bool> EnableDisableAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var currentBook = await _context.Books.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (currentBook != null)
                {
                    currentBook.IsActive = !currentBook.IsActive;
                    _context.Books.Update(currentBook);
                    await _context.SaveChangesAsync();
                    return true;    
                }
                else return false;
            }
            else return false;
        }

        public async Task <List<Book>> GetAllAsync()
        {
            return await _context.Books.Include("Category").ToListAsync();
        }   

        public async Task<bool> CreateBookAsync(CreateBookViewModel info, string creator, string categoryId)
        {
            try
            {
                Book newBook = new Book()
                {
                    Id = Guid.NewGuid().ToString(),
                    Creator = creator,
                    Name = info.name,
                    ImgUrl = info.imgUrl,
                    Author = info.Author,
                    PulishDate = info.PulishDate,
                    Pulisher = info.pulisher,
                    CategoryId = categoryId,
                    IsActive = true,
                };
                await _context.AddAsync(newBook);
                await _context.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
