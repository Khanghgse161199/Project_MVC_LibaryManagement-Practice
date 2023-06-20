using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.BookHistory
{
	public class CreateOrderViewModel
	{
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
		public string CitizenId { get; set; }
		[Required]
		public string studentCode { get; set; }
		[Required]
		public DateTime DatePay { get; set; }
	}
}
