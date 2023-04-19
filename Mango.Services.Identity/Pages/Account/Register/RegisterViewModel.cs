using Mango.Services.Identity.Pages.Attribute;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.Identity.Pages.Account.Register
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        [BindProperty]
        [Required]
        [PasswordAttribute(ErrorMessage = "При создании пароля используйте хотя бы один не буквенно-аримфметический символ")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public string RoleName { get; set; }


    }
}