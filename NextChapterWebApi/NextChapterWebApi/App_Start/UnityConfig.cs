using NextChapterWebApi.App_Config;
using NextChapterWebApi.DataAccess;
using NextChapterWebApi.Interfaces;
using NextChapterWebApi.Repositories;
using System.Data.Entity;
using Unity;
using Unity.Lifetime;

namespace NextChapterWebApi
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterComponents()
        {
			var container = new UnityContainer();            

            //register tyoes
            container.RegisterType<DbContext, CustomerPricingTaskEntities>(new PerThreadLifetimeManager());
            container.RegisterType<IProductRepository, ProductRepository>(new PerThreadLifetimeManager());
            container.RegisterType<ISpecificPriceRepository, SpecificPriceRepository>(new PerThreadLifetimeManager());

            var mapper = AutoMapperConfiguration.Configure();
            container.RegisterInstance(mapper);

            return container;
        }
    }
}