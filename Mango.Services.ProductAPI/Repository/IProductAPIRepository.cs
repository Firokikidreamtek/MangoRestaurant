using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI.Repository
{
    public interface IProductAPIRepository
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> GetById(int id);
        Task<ProductDto> Create(ProductDto productDto);
        Task<ProductDto> Update(ProductDto productDto);
        Task<bool> Delete(int id);
    }
}
