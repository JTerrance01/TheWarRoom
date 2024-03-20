using AutoMapper;
using MixWarz.Service.ProductAPI.Models;
using MixWarz.Service.ProductAPI.Models.Dto;

namespace MixWarz.Service.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Models.Product, ProductDto>();
            });
            return mappingConfig;
        }
    }
}
