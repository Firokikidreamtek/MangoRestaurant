using Mango.Web;
using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services;
using Mango.Web.Services.IServices;

public class AuthService : BaseService, IAuthService
{
    private readonly IHttpClientFactory _clientFactory;
    private string villaUrl;

    public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");

    }

    public Task<T> LoginAsync<T>(LoginRequestDto obj)
    {
        return SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = obj,
            Url = villaUrl + "/api/v1/UsersAuth/login"
        });
    }

    public Task<T> RegisterAsync<T>(RegisterationRequestDto obj)
    {
        return SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = obj,
            Url = villaUrl + "/api/v1/UsersAuth/register"
        });
    }
}