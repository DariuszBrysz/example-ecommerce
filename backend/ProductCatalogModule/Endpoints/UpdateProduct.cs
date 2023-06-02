namespace Backend.ProductCatalogModule
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UpdateProduct
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuthorizationService _authorizationService;

        public UpdateProduct(IProductRepository productRepository, IAuthorizationService authorizationService)
        {
            _productRepository = productRepository;
            _authorizationService = authorizationService;
        }

        public async Task<Product> ExecuteAsync(Guid id, Product product, ClaimsPrincipal user)
        {
            _authorizationService.Authorize(Permissions.UpdateProduct, user);
            var existingProduct = await _productRepository.GetAsync(id) ?? throw new ProductNotFoundException(id);
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;

            await _productRepository.UpdateAsync(existingProduct);

            return existingProduct;
        }
    }
}