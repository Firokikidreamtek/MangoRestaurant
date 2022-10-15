using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.Identity.MainModule.Account.Attribute
{
    public class Password : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            RegisterViewModel data = (RegisterViewModel)value;

            if (
                data.Password.Any(x => char.IsUpper(x)) 
                && data.Password.Any(char.IsDigit) 
                && data.Password.Any(char.IsLetter)
                && data.Password.Contains('*')
                )
            {
                return true;
            }
            ErrorMessage = "Пароль должен содержать как минимум 1 заглавную букву 1 цифру и 1 спец символ";
            return false;
        }
    }
}
