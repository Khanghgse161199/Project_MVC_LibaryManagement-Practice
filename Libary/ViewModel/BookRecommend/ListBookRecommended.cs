namespace Libary.ViewModel.BookRecommend
{
	public class ListBookRecommended
	{
		public List<BookRecommendViewModel> books { get; set; }	
		
		public ListBookRecommended()
		{
			books = new List<BookRecommendViewModel>();
		}
	}
	public class BookRecommendViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string ImgUrl { get; set; }

		public string Author { get; set; }

		public DateTime PulishDate { get; set; }

		public string Pulisher { get; set; }

		public string CategoryName { get; set; }
	}
}
