using DataService.Entities;

namespace Libary.ViewModel.OrderViewModel
{

    public class ListOrderViewModel
    {
        public List<OrderViewModel> orderViewModels { get; set; }
        public ListOrderViewModel()
        {
            orderViewModels = new List<OrderViewModel>();
        }
    }
    public class OrderViewModel
    {
        public string Id { get; set; } = null!;

        public string CardId { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public bool IsAccept { get; set; }

        public bool IsActive { get; set; }

        public string CitizenId { get; set; } = null!;

        public string StudentCode { get; set; } = null!;

        public DateTime DatePay { get; set; }

        public bool IsPay { get; set; }

        public string PhoneNum { get; set; } = null!;

        public virtual Card Card { get; set; } = null!;

        public List<DataService.Entities.History>? Historys { get; set; }
    }
}
