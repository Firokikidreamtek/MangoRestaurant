using Mango.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        public Task<T> GetAllAsync<T>();
        public Task<T> GetByIdAsync<T>(int id);
        public Task<T> CreateProductAsync<T>(ProductDto product);
        public Task<T> UpdateProductAsync<T>(ProductDto product);
        public Task<T> DeleteProductAsync<T>(int id);

    }
}
