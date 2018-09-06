using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace AutomobileCMS.Models
{
    public class LoginVM
    {
        [HiddenInput(DisplayValue = false)]
        public string username { get; set; }

        [ReadOnly(true)]
        public string Password { get; set; }

        public string CurrentStatus { get; set; }
    }
}