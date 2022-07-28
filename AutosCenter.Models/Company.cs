using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosCenter.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }    

        public string? StreetAddress { get; set; } 
        public string? City { get; set; }    
        public string? State { get; set; }   
        public string? PostalCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{4})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number")]
        public string? PhoneNumber { get; set; }
    }
}
