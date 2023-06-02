using System.Security.Claims;

namespace Backend.ProductCatalogModule;

public class GetProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IAuthorizationService _authorizationService;

    public GetProduct(IProductRepository productRepository, IAuthorizationService authorizationService)
    {
        _productRepository = productRepository;
        _authorizationService = authorizationService;
    }

    public async Task<Product> ExecuteAsync(Guid id, ClaimsPrincipal user)
    {
        _authorizationService.Authorize(Permissions.GetProduct, user);
        return await _productRepository.GetAsync(id);
    }
}