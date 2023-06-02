using System.Security.Claims;

namespace Backend.ProductCatalogModule;

public class ListProducts
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public ListProducts(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<IEnumerable<Product>> ExecuteAsync(ClaimsPrincipal user)
    {
        _authorizationService.Authorize(Permissions.ListProducts, user);
        return await _productRepository.ListAsync();
    }
}