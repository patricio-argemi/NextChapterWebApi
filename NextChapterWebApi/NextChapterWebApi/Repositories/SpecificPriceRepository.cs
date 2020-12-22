using AutoMapper;
using NextChapterWebApi.DataAccess;
using NextChapterWebApi.Interfaces;
using NextChapterWebApi.Models;
using System;
using System.Linq;
using Unity;

namespace NextChapterWebApi.Repositories
{
    ///<inheritdoc cref="ISpecificPriceRepository"/>
    public class SpecificPriceRepository : IDisposable, ISpecificPriceRepository
    {
        private CustomerPricingTaskEntities _databaseContext;
        private Mapper _mapper;

        public SpecificPriceRepository(Mapper mapper) : base()
        {
            _mapper = mapper;
            GetDependentInstances();
        }

        public void Add(SpecificPrice newSpecificPrice)
        {
            SPECIFIC_PRICE specificPriceDataModel = _mapper.Map<SpecificPrice, SPECIFIC_PRICE>(newSpecificPrice);

            _databaseContext.SPECIFIC_PRICE.Add(specificPriceDataModel);
            _databaseContext.SaveChanges();
        }

        public void Update(SpecificPrice updatedSpecificPrice)
        {
            var specificPriceToUpdate = _databaseContext.SPECIFIC_PRICE.FirstOrDefault(p => p.Product_Code.Equals(updatedSpecificPrice.Product.Code) &&
                                                                                            p.Customer_Name.Equals(updatedSpecificPrice.Customer.Name));
            specificPriceToUpdate.Product_Price = updatedSpecificPrice.Product.Price;
            _databaseContext.SaveChanges();
        }

        public void Delete(SpecificPrice specificPriceToDelete)
        {
            var matchingSpecificPrice = _databaseContext.SPECIFIC_PRICE
                    .FirstOrDefault(d => d.Customer_Name.Equals(specificPriceToDelete.Customer.Name) &&
                                d.Product_Code.Equals(specificPriceToDelete.Product.Code));

            _databaseContext.Entry(matchingSpecificPrice).State = System.Data.Entity.EntityState.Deleted;
            _databaseContext.SaveChanges();
        }

        public Tuple<decimal?, bool> GetByCustomerAndProductCode(SpecificPrice specificPrice)
        {
            SpecificPrice matchingSpecificPrice = null;

            var matchingSpecificPriceDataModel = _databaseContext.SPECIFIC_PRICE
                    .FirstOrDefault(d => d.Customer_Name.Equals(specificPrice.Customer.Name) &&
                                d.Product_Code.Equals(specificPrice.Product.Code));

            if (matchingSpecificPriceDataModel != null)
            {
                matchingSpecificPrice = new SpecificPrice()
                {
                    Customer = new Customer() { Name = matchingSpecificPriceDataModel.Customer_Name },
                    Product = new Product() { Code = matchingSpecificPriceDataModel.Product_Code, Price = matchingSpecificPriceDataModel.Product_Price }
                };
            }

            if (matchingSpecificPrice == null)
            {
                //check if base price exists for product code
                var basePriceProduct = _databaseContext.PRODUCT.First(p => p.Code.Equals(specificPrice.Product.Code));

                if (basePriceProduct.Price >= 0)
                {
                    return new Tuple<decimal?, bool>(basePriceProduct.Price, false);
                }
            }

            return new Tuple<decimal?, bool>(matchingSpecificPrice.Product.Price, true);
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