using System.Security.Claims;

namespace Backend.InventoryModule;

public class AddStock
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IAuthorizationService _authorizationService;

    public AddStock(IInventoryRepository inventoryRepository, IAuthorizationService authorizationService)
    {
        _inventoryRepository = inventoryRepository;
        _authorizationService = authorizationService;
    }

    public async Task<int> ExecuteAsync(Guid productId, int quantity, ClaimsPrincipal user)
    {
        _authorizationService.Authorize(Permissions.AddStock, user);
        return await _inventoryRepository.AddStockAsync(productId, quantity);
    }
}