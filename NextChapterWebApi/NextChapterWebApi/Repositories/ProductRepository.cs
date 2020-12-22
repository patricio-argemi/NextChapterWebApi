using AutoMapper;
using NextChapterWebApi.DataAccess;
using NextChapterWebApi.Interfaces;
using NextChapterWebApi.Models;
using System;
using System.Linq;
using Unity;

namespace NextChapterWebApi.Repositories
{
    ///<inheritdoc cref="IProductRepository"/>
    public class ProductRepository : IDisposable, IProductRepository
    {
        private CustomerPricingTaskEntities _databaseContext;
        private Mapper _mapper;

        public ProductRepository(Mapper mapper, CustomerPricingTaskEntities databaseContext) : base()
        {
            _mapper = mapper;

            if (databaseContext != null)
            {
                _databaseContext = databaseContext;
            }
            else
            {
                GetDependentInstances();
            }            
        }

        public void Add(Product newProduct)
        {
            PRODUCT productDataModel = _mapper.Map<Product, PRODUCT>(newProduct);

            _databaseContext.PRODUCT.Add(productDataModel);
            _databaseContext.SaveChanges();
        }

        public void Update(Product updatedProduct)
        {
            var productToUpdate = _databaseContext.PRODUCT.FirstOrDefault(p => p.Code.Equals(updatedProduct.Code));           
            productToUpdate.Price = updatedProduct.Price;
            _databaseContext.SaveChanges();
        }

        public Product GetByProductCode(string productCode)
        {
            var foundProductDataModel = _databaseContext.PRODUCT.FirstOrDefault(p => p.Code.Equals(productCode));

            var foundProductDto = _mapper.Map<PRODUCT, Product>(foundProductDataModel);

            return foundProductDto;
        }

        private void GetDependentInstances()
        {
            var unityContainer = UnityConfig.RegisterComponents();
            _databaseContext = unityContainer.Resolve<CustomerPricingTaskEntities>();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_databaseContext != null)
                {
                    _databaseContext.Dispose();
                    _databaseContext = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}