using NextChapterWebApi.Models;

namespace NextChapterWebApi.Interfaces
{
    /// <summary>
    /// Product Repository Interface.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Creates a new Product.
        /// </summary>
        /// <param name="newProduct">The Product to be created.</param>
        void Add(Product newProduct);

        /// <summary>
        /// Updates a Product.
        /// </summary>
        /// <param name="updatedProduct">The Product data to be updated.</param>
        void Update(Product updatedProduct);

        /// <summary>
        /// Gets a Product by Product Code.
        /// </summary>
        /// <param name="productCode">The Product Code.</param>
        /// <returns>The updated Product.</returns>
        Product GetByProductCode(string productCode);
    }
}
