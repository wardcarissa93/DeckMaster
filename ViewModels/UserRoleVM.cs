using System.ComponentModel.DataAnnotations;

namespace DeckMaster.ViewModels
{
    public class UserRoleVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role Name is required.")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
