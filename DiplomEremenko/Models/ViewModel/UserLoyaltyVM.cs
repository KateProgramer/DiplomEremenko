
using System.ComponentModel.DataAnnotations;

namespace DiplomEremenko.Models.ViewModel
{
    public class UserLoyaltyVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Phone { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
    }
}
