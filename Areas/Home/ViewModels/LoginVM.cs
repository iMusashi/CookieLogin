using System.ComponentModel.DataAnnotations;

namespace AutomobileCMS.Areas.Home.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }

    //TODO: Do custom validation here
}
