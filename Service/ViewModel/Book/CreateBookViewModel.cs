using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Book
{
    public class CreateBookViewModel
    {
        [Required]
        public string name {  get; set; }
        [Required]
        public string imgUrl { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime PulishDate { get; set; }
        [Required]
        public string pulisher { get; set; }
        [Required]
        public string CategoryId { get; set; }
    }
}
