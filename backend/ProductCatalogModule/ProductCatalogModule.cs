using System.Security.Claims;

namespace Backend.ProductCatalogModule;

public class ProductCatalogModule : IModule<IAuthorizationService>
{
    public ApiEndpoint[] AddModule(IAuthorizationService authorizationService)
    {
        return new[]
        {
            new ApiEndpoint("/products", (string? name, decimal? minPrice, decimal? maxPrice, bool? onlyInStock, IProductRepository productRepository, ClaimsPrincipal user) => {
                if (name is not null || minPrice is not null || maxPrice is not null || onlyInStock is not null)
                {
                    return new ListProductsWithFilters(productRepository, authorizationService).ExecuteAsync(user, name, minPrice, maxPrice, onlyInStock);
                }
                else
                {
                    return new ListProducts(productRepository, authorizationService).ExecuteAsync(user);
                }
            }),
            new ApiEndpoint("/products/{id}", (Guid id, IProductRepository productRepository, ClaimsPrincipal user) => new GetProduct(productRepository, authorizationService).ExecuteAsync(id, user)),
            new ApiEndpoint("/products", (Product product, IProductRepository productRepository, ClaimsPrincipal user) => new CreateProduct(productRepository, authorizationService).ExecuteAsync(product, user), HttpMethod.Post),
            new ApiEndpoint("/products/{id}", (Guid id, Product product, IProductRepository productRepository, ClaimsPrincipal user) => new UpdateProduct(productRepository, authorizationService).ExecuteAsync(id, product, user), HttpMethod.Put),
        };
    }
}