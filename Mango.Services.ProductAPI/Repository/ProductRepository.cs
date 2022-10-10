using AutoMapper;
using Mango.Services.ProductAPI.DBContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> Create(ProductDto productDto)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(product => product.Name == productDto.Name);
            if (product == null)
            {
                Product newProduct = _mapper.Map<ProductDto, Product>(productDto);
                _db.Products.Add(newProduct);
                await _db.SaveChangesAsync();
            }
            return productDto;
        }
        public async Task<ProductDto> Update(ProductDto productDto)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(product => product.Name == productDto.Name);
            if (product != null)
            {
                Product newProduct = _mapper.Map<ProductDto, Product>(productDto);
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.CategoryName = productDto.CategoryName;
                product.ImageUrl = productDto.ImageUrl;
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
            }
            return productDto;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Product product = await _db.Products.FirstOrDefaultAsync(product => product.Id == id);
                if (product == null)
                {
                    return false;
                }
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ProductDto> GetById(int id)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(product => product.Id == id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            List<Product> productList = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }

    }
}
