using DataService.Entities;

namespace AdminLibary.ViewModel.OrderViewModelAdmin
{

    public class ListOrderViewModelAdmin
    {
        public List<OrderViewModelAdmin> orderViewModelAdmins { get; set; }
        public ListOrderViewModelAdmin()
        {
            orderViewModelAdmins = new List<OrderViewModelAdmin>();
        }
    }
    public class OrderViewModelAdmin
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
