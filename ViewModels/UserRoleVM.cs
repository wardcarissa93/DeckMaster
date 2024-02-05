using System.ComponentModel.DataAnnotations;

namespace DeckMaster.ViewModels
{
    public class UserRoleVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
