using System.ComponentModel.DataAnnotations;

namespace rapid.erp.ViewModel.Security
{
    public partial class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
