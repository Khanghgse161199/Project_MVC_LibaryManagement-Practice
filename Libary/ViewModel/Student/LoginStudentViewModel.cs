using System.ComponentModel.DataAnnotations;

namespace Libary.ViewModel.Student
{
	public class LoginStudentViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string password { get; set; }	
	}
}
