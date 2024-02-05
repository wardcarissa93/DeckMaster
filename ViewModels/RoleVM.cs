using System.ComponentModel.DataAnnotations;

namespace DeckMaster.ViewModels
{
    public class RoleVM
    {
        [Key]
        [Display(Name = "Id")]
        public string RoleId { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
