using NextChapterWebApi.Interfaces;
using NextChapterWebApi.Models;
using System.Net;
using System.Web.Http;
using Unity;

namespace NextChapterWebApi.Controllers
{
    /// <summary>
    /// Products Endpoint.
    /// </summary>
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {        
        private IProductRepository _productRepository;

        public ProductController() : base()
        {
            GetDependentInstances();
        }

        public ProductController(IProductRepository productRepository) : base()
        {
            _productRepository = productRepository;            
        }

        /// <summary>
        /// Creates a Product.
        /// </summary>
        /// <param name="newProduct">The new Product to be added.</param>
        /// <returns>IHttpActionResult object.</returns>
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateProduct(Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data provided.");
            }

            _productRepository.Add(newProduct);

            return Content(HttpStatusCode.OK, string.Format("Product {0} added successfully.", newProduct.Code));            
        }

        /// <summary>
        /// Updates Product Price.
        /// </summary>
        /// <param name="updatedProduct">The Product with updated data.</param>
        /// <returns>IHttpActionResult object.</returns>
        [HttpPut]
        [Route("update")]
        public IHttpActionResult UpdateProductPrice(Product updatedProduct)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data provided.");
            }

            _productRepository.Update(updatedProduct);

            return Content(HttpStatusCode.OK, string.Format("Product {0} price updated to ${1}", updatedProduct.Code, updatedProduct.Price));
        }

        /// <summary>
        /// Find Product by Code.
        /// </summary>
        /// <param name="productCode">The Product Code.</param>
        /// <returns>IHttpActionResult object.</returns>
        [HttpGet]
        [Route("price/{productCode}")]
        public IHttpActionResult FindProductPriceByCode(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                return Content(HttpStatusCode.BadRequest, "Please provide a product code.");
            }

            var matchingProduct = _productRepository.GetByProductCode(productCode);

            if (matchingProduct == null)
            {
                return Content(HttpStatusCode.BadRequest, string.Format("No products found with code {0}.", productCode));
            }

            return Ok(matchingProduct.Price);
        }

        private void GetDependentInstances()
        {
            var unityContainer = UnityConfig.RegisterComponents();
            _productRepository = unityContainer.Resolve<IProductRepository>();
        }
    }
}
