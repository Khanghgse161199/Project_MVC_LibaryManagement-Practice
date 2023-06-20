using System.ComponentModel.DataAnnotations;

namespace AdminLibary.ViewModel.User
{
    public class CreateUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Fullname { get; set; }
    }
}
