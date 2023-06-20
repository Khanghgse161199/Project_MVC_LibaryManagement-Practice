namespace AdminLibary.ViewModel.User
{
    public class ListUserViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public ListUserViewModel()
        {
            Users = new List<UserViewModel>();
        }
    }
    public class UserViewModel
    {
        public string Id { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
