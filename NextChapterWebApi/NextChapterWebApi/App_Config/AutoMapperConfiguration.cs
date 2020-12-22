using AutoMapper;
using NextChapterWebApi.DataAccess;
using NextChapterWebApi.Models;

namespace NextChapterWebApi.App_Config
{
    public static class AutoMapperConfiguration
    {
        private static Mapper _mapper;
        public static Mapper Configure()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PRODUCT, Product>();
                cfg.CreateMap<Product, PRODUCT>();
                cfg.CreateMap<SPECIFIC_PRICE, SpecificPrice>();
                cfg.CreateMap<SpecificPrice, SPECIFIC_PRICE>();
            });

            _mapper = (Mapper)config.CreateMapper();

            return _mapper;
        }
    }
}