﻿using Mango.Web.Models.Dto;

namespace Mango.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetByUserIdAsync<T>(string userId, string token = null);
        Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> UpdateAsync<T>(CartDto cartDto, string token = null);
        Task<T> DecreaseAsync<T>(int cartId, string token = null);
        Task<T> ApplyCoupon<T>(CartDto cartDto, string token = null);
        Task<T> RemoveCoupon<T>(string userId, string token = null);
        Task<T> Checkout<T>(CartHeaderDto cartHeader, string token = null);
    }
}
