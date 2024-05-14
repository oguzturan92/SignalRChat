using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class UserRegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string RePassword { get; set; }
        public string RoleName { get; set; }
    }
}