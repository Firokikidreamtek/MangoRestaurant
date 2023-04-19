using Mango.Web.Models.Dto;
using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllAsync<ResponseDto>(await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {

            ProductDto model = new();
            string token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetByIdAsync<ResponseDto>(id, token);
            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            return View(model);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Details(ProductDto productDto)
        {
            CartDto cartDto = new()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == "sid")?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId
            };

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var resp = await _productService.GetByIdAsync<ResponseDto>(productDto.ProductId, accessToken);
            if (resp != null && resp.IsSuccess)
            {
                cartDetails.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resp.Result));
            }
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetails);
            cartDto.CartDetails = cartDetailsDtos;

            var addToCartResp = await _cartService.AddToCartAsync<ResponseDto>(cartDto, accessToken);
            if (addToCartResp != null && addToCartResp.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        //[Authorize]
        //public async Task<IActionResult> Login()
        //{
        //    var accessToken = await HttpContext.GetTokenAsync("access_token");
        //    return RedirectToAction(nameof(Index), "Home");
        //}

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    SignOut("Cookies", "oidc");
        //    return RedirectToAction("Index", "Home");
        //}
        public IActionResult Privacy()
        {
            return View();
        }
    }
}