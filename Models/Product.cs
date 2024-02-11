using System.ComponentModel.DataAnnotations;

namespace DeckMaster.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required] // Added for validation
        public string ProductName { get; set; }

        [Required] // Added for validation
        public string Description { get; set; }

        [Required] // Added for validation
        public string Price { get; set; }

        [Required] // Added for validation
        public string Currency { get; set; }

        [Required] // Added for validation
        public string ImageName { get; set; }
    }
}
