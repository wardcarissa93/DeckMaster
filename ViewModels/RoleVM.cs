using System.ComponentModel.DataAnnotations;

namespace DeckMaster.ViewModels
{
    public class RoleVM
    {
        [Key]
        [Display(Name = "Id")]
        [StringLength(2, ErrorMessage = "The Id cannot be more than 2 characters.")]
        public string RoleId { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
