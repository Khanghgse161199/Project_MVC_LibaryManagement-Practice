namespace AdminLibary.ViewModel.BookViewModel
{
    public class ListBookViewModel
    {
        public List<BookViewModel> Books { get; set; }

        public ListBookViewModel()
        {
            Books = new List<BookViewModel>();
        }
    }
    public class BookViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public string Author { get; set; }

        public DateTime PulishDate { get; set; }

        public string Pulisher { get; set; }

        public string CategoryName { get; set; } 
        public bool isActive { get; set; } 
    }
}
