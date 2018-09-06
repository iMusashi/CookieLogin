using System.ComponentModel.DataAnnotations;

namespace AutomobileCMS.ViewModels
{
    public class SecurityEntry
    {
        [Display(Name = "Registration No.")]
        [RegularExpression("^[a-zA-Z0-9]{1,10}$", ErrorMessage ="Maximum 10 characters allowed.")]
        public string RegistrationNo { get; set; }
    }
}