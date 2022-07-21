

using System.ComponentModel.DataAnnotations;

namespace AutosCenter.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public string Model { get; set; }

        public string Year { get; set; }

        public string Manufacturer { get; set; }
        [Required]
        [Range(1,10000000)]
        [Display(Name="List Price")]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000000)]
        public double Price { get;set; }
        [Display(Name="Image")]
        public string ImageUrl { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        [Required]
        [Display(Name="Cover Type")]
        public int CoverTypeId { get; set; }

        public CoverType CoverType { get; set; }
    }
}
