namespace AdminLibary.ViewModel.CategoryViewmodel
{
    public class ListCategoryViewModel
    {
        public List<CategoryViewModel> categorys { get; set; }
        public ListCategoryViewModel()
        {
            categorys = new List<CategoryViewModel>();
        }
    }
    public class CategoryViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
    }
}
