namespace Libary.ViewModel.CardViewModel
{
	public class ListBookHistoryViewModel
	{
		public List<BookViewModel> Books { get; set; }
		public ListBookHistoryViewModel() { 
			Books = new List<BookViewModel>();
		}
	}

	public class BookViewModel
	{
		public string id { get; set; }
		public string name { get; set; }
		public string ImgUrl { get; set; }
		public int number { get; set; }
	}
}
