using Mango.Services.Identity.Pages.Account.Register;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.Identity.Pages.Attribute
{
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string data = (string)value;

            if (!data.Contains('@'))
            {
                return false;
            }
            return true;
        }
    }
}