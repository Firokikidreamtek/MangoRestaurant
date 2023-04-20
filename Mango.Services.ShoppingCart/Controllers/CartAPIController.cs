using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.Messages;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.RabbitMQSender;
using Mango.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController : Controller
    {
        private readonly ICartAPIRepository _cartRepository;
        private readonly IMessageBus _messageBus;
        private readonly ICouponRepository _couponRepository;
		private readonly IRabbitMQCartMessageSender _rabbitMQCartMessageSender;
		protected ResponseDto _response;

		public CartAPIController(ICartAPIRepository cartRepository, 
                                 IMessageBus messageBus, 
                                 ICouponRepository couponRepository, 
                                 IRabbitMQCartMessageSender rabbitMQCartMessageSender)
		{
			_cartRepository = cartRepository;
			_rabbitMQCartMessageSender = rabbitMQCartMessageSender;
			_couponRepository = couponRepository;
			_messageBus = messageBus;
			_response = new ResponseDto();
		}

		[HttpGet]
        [Route("{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetByUserId(userId);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<object> AddCart([FromBody]CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdate(cartDto);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch]
        public async Task<object> UpdateCart([FromBody]CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdate(cartDto);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Route("RemoveCart/{cartId}")]
        public async Task<object> RemoveDetail(int cartId)
        {
            try
            {
                bool isSuccess = await _cartRepository.Decrease(cartId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Route("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                bool isSuccess = await _cartRepository.ApplyCoupon(cartDto.CartHeader.UserId,
                    cartDto.CartHeader.CouponCode);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Route("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Route("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDto checkoutHeader)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetByUserId(checkoutHeader.UserId);
                if (cartDto == null)
                {
                    return BadRequest();
                }

                if (!string.IsNullOrEmpty(checkoutHeader.CouponCode))
                {
                    CouponDto coupon = await _couponRepository.GetCoupon(checkoutHeader.CouponCode);
                    if (checkoutHeader.DiscountTotal != coupon.DiscountAmount)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string>() { "Coupon Price has changed, please confirm" };
                        _response.Message = "Coupon Price has changed, please confirm";
                        return _response;
                    }
                }

                checkoutHeader.CartDetails = cartDto.CartDetails;
				//logic to add message to process order.
				//await _messageBus.PublishMessage(checkoutHeader, "checkoutmessagetopic");

				_rabbitMQCartMessageSender.SendMessage(checkoutHeader, "checkoutqueue");
				await _cartRepository.Clear(checkoutHeader.UserId);
			}
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}