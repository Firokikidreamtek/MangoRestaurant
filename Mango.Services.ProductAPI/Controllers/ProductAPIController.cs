using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : Controller
    {
        private ResponseDto _response;
        private IProductAPIRepository _productRepository;

        public ProductAPIController(IProductAPIRepository productRepository)
        {
            _response = new ResponseDto();
            _productRepository = productRepository;
        }

        [NonAction]
        public ObjectResult SetError(Exception e)
        {
            return StatusCode(500, e.Message);
        }

        [HttpGet]
        public async Task<object> GetAll()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _productRepository.GetAll();
                _response.Result = productDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetById(int id)
        {
            try
            
            {
                ProductDto productDto = await _productRepository.GetById(id);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<object> Create([FromBody] ProductDto productDto)
        {
            try
            {
                var model = await _productRepository.Create(productDto);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<object> Update([FromBody] ProductDto productDto)
        {
            try
            {
                var model = await _productRepository.Update(productDto);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool response = await _productRepository.Delete(id);
                _response.Result = response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }


    }
}
