using System.ComponentModel.DataAnnotations;

namespace DeckMaster.ViewModels
{
    public class UserVM
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
