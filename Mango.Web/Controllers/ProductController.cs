using Mango.Web.Models.Dto;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(product);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _productService.GetByIdAsync<ResponseDto>(id);
            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDto>(product);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var response = DeleteProduct(id).Result;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        private async Task<ResponseDto> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProductAsync<ResponseDto>(id);
            return response;
        }


    }
}
