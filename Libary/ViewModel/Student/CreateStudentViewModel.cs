using System.ComponentModel.DataAnnotations;

namespace Libary.ViewModel.Student
{
	public class CreateStudentViewModel
	{
		[Required]
		public string username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string password { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string email { get; set; }
	}
}
