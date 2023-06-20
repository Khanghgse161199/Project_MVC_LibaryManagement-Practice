using System.ComponentModel.DataAnnotations;

namespace Libary.ViewModel.Historys
{
	public class CreateHistoryViewModel
	{
		[Required]
		public string bookId { get; set; }
	}
}
