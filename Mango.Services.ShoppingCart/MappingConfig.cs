using AutoMapper;

namespace Mango.Services.ShoppingCart
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mapConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mapConfig;
        }
    }
}
