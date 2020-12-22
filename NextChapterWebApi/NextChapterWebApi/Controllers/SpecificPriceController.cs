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
    [RoutePrefix("api/pricing")]
    public class SpecificPriceController : ApiController
    {
        private ISpecificPriceRepository _specificPriceRepository;

        public SpecificPriceController() : base()
        {
            GetDependentInstances();
        }

        /// <summary>
        /// Creates a Specific Price.
        /// </summary>
        /// <param name="newSpecificPrice">The new Specific Price to be added.</param>
        /// <returns>IHttpActionResult object.</returns>
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateSpecificPrice(SpecificPrice newSpecificPrice)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data provided.");
            }

            _specificPriceRepository.Add(newSpecificPrice);

            return Content(HttpStatusCode.OK, string.Format("Specific price for Customer {0} added successfully. Product code: {1} - Specific Price: {2}",
                newSpecificPrice.Customer.Name, newSpecificPrice.Product.Code, newSpecificPrice.Product.Price));
        }

        /// <summary>
        /// Updates Specific Price.
        /// </summary>
        /// <param name="updatedSpecificPrice">The Specific Price with updated data.</param>
        /// <returns>IHttpActionResult object.</returns>
        [HttpPut]
        [Route("update")]
        public IHttpActionResult UpdateSpecificPrice(SpecificPrice updatedSpecificPrice)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data provided.");
            }

            _specificPriceRepository.Update(updatedSpecificPrice);

            return Content(HttpStatusCode.OK, string.Format("Specific Price updated for Customer {0} and Product {1}. Price updated to ${2}", 
                updatedSpecificPrice.Customer.Name, updatedSpecificPrice.Product.Code, updatedSpecificPrice.Product.Price));
        }

        /// <summary>
        /// Deletes Specific Price.
        /// </summary>
        /// <param name="specificPriceToDelete">The Specific Price data to delete.</param>
        /// <returns>IHttpActionResult object.</returns>
        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteSpecificPriceByCustomerAndProductCode(SpecificPrice specificPriceToDelete)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data provided.");
            }

            _specificPriceRepository.Delete(specificPriceToDelete);

            return Content(HttpStatusCode.OK, string.Format("Specific Price for Product {0} to Customer {1} deleted successfully.",
                specificPriceToDelete.Product.Code, specificPriceToDelete.Customer.Name));
        }

        /// <summary>
        /// Find Specific Price by Customer and Product Code.
        /// </summary>
        /// <param name="specificPrice">The Specific Price object containing the Customer and Product codes.</param>
        /// <returns>IHttpActionResult object.</returns>
        [HttpGet]
        [Route("price")]
        public IHttpActionResult FindSpecificPriceByCustomerAndProductCode(SpecificPrice specificPrice)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data provided.");
            }

            var result = _specificPriceRepository.GetByCustomerAndProductCode(specificPrice);

            //success
            if (result.Item2)
            {
                return Content(HttpStatusCode.OK, string.Format("Specific Price of {0} found for Customer {1} and Product Code {2}.",
                result.Item1, specificPrice.Customer.Name, specificPrice.Product.Code));
            }
            //base price
            else
            {
                return Content(HttpStatusCode.OK, string.Format("Specific Price not found for Customer {0} and Product Code {1}. Base price for that product code is ${2}.",
                specificPrice.Customer.Name, specificPrice.Product.Code, result.Item1));
            }            
        }

        private void GetDependentInstances()
        {
            var unityContainer = UnityConfig.RegisterComponents();
            _specificPriceRepository = unityContainer.Resolve<ISpecificPriceRepository>();
        }
    }
}
