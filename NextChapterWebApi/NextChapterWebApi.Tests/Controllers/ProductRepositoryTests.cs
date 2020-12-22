using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextChapterWebApi.App_Config;
using NextChapterWebApi.DataAccess;
using NextChapterWebApi.Models;
using NextChapterWebApi.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextChapterWebApi.Tests.Controllers
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private DbSet<PRODUCT> _productDbSetMock;
        private CustomerPricingTaskEntities _customerPricingTaskDbContextMock;
        private ProductRepository _productRepositoryMock;
        private List<PRODUCT> _productsListMock;

        [TestInitialize]
        public void Initialize()
        {
            var _mapper = AutoMapperConfiguration.Configure();

            _customerPricingTaskDbContextMock = Substitute.For<CustomerPricingTaskEntities>();
            _productRepositoryMock = new ProductRepository(_mapper, _customerPricingTaskDbContextMock);

            _productsListMock = new List<PRODUCT>
            {
                new PRODUCT { Code = "1", Price = 1m},
                new PRODUCT { Code = "2", Price = 2m},
                new PRODUCT { Code = "3", Price = 3m}
            };

            _productDbSetMock = Substitute.For<DbSet<PRODUCT>, IQueryable<PRODUCT>>();
            ((IQueryable<PRODUCT>)_productDbSetMock).Provider.Returns(_productsListMock.AsQueryable().Provider);
            ((IQueryable<PRODUCT>)_productDbSetMock).Expression.Returns(_productsListMock.AsQueryable().Expression);
            ((IQueryable<PRODUCT>)_productDbSetMock).ElementType.Returns(_productsListMock.AsQueryable().ElementType);
            ((IQueryable<PRODUCT>)_productDbSetMock).GetEnumerator().Returns(_productsListMock.AsQueryable().GetEnumerator());

            _productDbSetMock.When(q => q.Add(Arg.Any<PRODUCT>()))
                .Do(q => _productsListMock.Add(q.Arg<PRODUCT>()));            

            //_productRepositoryMock.When(q => q.Update(Arg.Any<Product>()))
            //    .Do(q => _productRepositoryMock.Update(q.Arg<Product>()));

            //_productRepositoryMock.When(q => q.GetByProductCode(Arg.Any<string>()))
            //    .Do(q => _productRepositoryMock.GetByProductCode(q.Arg<string>()));


            _customerPricingTaskDbContextMock.PRODUCT = _productDbSetMock;           
        }

        [TestMethod]
        public void AddShouldReceiveAProductAndSaveChanges()
        {
            // Act
            _productRepositoryMock.Add(new Models.Product() { Code = "4", Price = 4m });

            // Assert mock products list received a call to add a PRODUCT, and the DB Context received a call to SaveChanges
            //_productDbSetMock.Received(1).Add(Arg.Any<PRODUCT>());
            _customerPricingTaskDbContextMock.Received(1).SaveChanges();

            Assert.IsTrue(_productsListMock.Any(p => p.Code.Equals("4")));
        }

        [TestMethod]
        public void UpdateShouldReceiveAValidProductAndSaveChanges()
        {
            // Act
            _productRepositoryMock.Update(new Models.Product() { Code = "3", Price = 7.6m });

            // Assert product repository received a call to update a Product, the DB Context received a call to SaveChanges 
            // and the mock products list item was updated successfully
            
            //_productRepositoryMock.Received(1).Update(Arg.Any<Product>());
            
            _customerPricingTaskDbContextMock.Received(1).SaveChanges();  

            Assert.IsTrue(_productsListMock.Any(p => p.Code.Equals("3") && p.Price.Equals(7.6m)));
        }

        [TestMethod]
        public void GetByProductCodeShouldReceiveAValidProductReturnAnUpdatedProduct()
        {
            // Act
            var foundProduct = _productRepositoryMock.GetByProductCode("1");

            // Assert product repository got a call to get a product by code with a string parameter, and that the code found is correct
            
            //_productRepositoryMock.Received(1).GetByProductCode(Arg.Any<string>());

            Assert.IsTrue(foundProduct.Price.Equals(1m));
        }
    }
}
