using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public interface ICartAPIRepository
    {
        Task<CartDto> GetByUserId(string userId);
        Task<CartDto> CreateUpdate(CartDto cartDto);
        Task<bool> Decrease(int cartDetailsId);
        Task<bool> Clear(string userId);
    }
}