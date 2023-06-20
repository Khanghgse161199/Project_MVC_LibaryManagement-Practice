using DataService.Entities;

namespace AdminLibary.ViewModel.Book
{
    public class CreateBookViewModel
    {
        public List<Category>? categories { get; set; }
        public Service.ViewModel.Book.CreateBookViewModel? info { get; set; }
    }

}
