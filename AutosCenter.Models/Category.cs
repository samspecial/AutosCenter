using System.ComponentModel.DataAnnotations;

namespace AutosCenter.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Display(Name = "display order")]
        [Range(1, 200, ErrorMessage = "Number cannot be greater than 200")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
