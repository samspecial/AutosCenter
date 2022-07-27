using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosCenter.Models.ViewModels
{
    public class DetailsPageViewModel
    {
        [Range(1, 2000, ErrorMessage="Please enter a value between 1 and 2000")]
        public int Count { get; set; } 
        public Product Product { get; set; }
    }
}
