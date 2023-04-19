using Mango.Web.Models.Dto;

namespace Mango.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto objToCreate);
        Task<T> RegisterAsync<T>(RegisterationRequestDto objToCreate);
    }
}