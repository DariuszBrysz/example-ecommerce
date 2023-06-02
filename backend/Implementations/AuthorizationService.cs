using Backend.Model;
using System.Security.Claims;

namespace Backend.Implementations;

public class AuthorizationService
{
    public void Authorize(Permission permission, ClaimsPrincipal user)
    {
        // Do nothing
        // In regular implementation, this would throw an exception if the user is not authorized
        switch (permission)
        {
            case Permission.GetProduct:
                break;
            case Permission.ListProducts:
                break;
            case Permission.ListProductsWithFilters:
                break;
            case Permission.CreateProduct:
                if (!user.IsInRole("admin"))
                    throw new UnauthorizedAccessException();
                break;
            case Permission.UpdateProduct:
                if (!user.IsInRole("admin"))
                    throw new UnauthorizedAccessException();
                break;
            case Permission.AddStock:
                if (!user.IsInRole("admin"))
                    throw new UnauthorizedAccessException();
                break;
            case Permission.RemoveStock:
                if (!user.IsInRole("admin"))
                    throw new UnauthorizedAccessException();
                break;
            case Permission.UploadImage:
                if (!user.IsInRole("admin"))
                    throw new UnauthorizedAccessException();
                break;
            default:
                throw new NotImplementedException();
        }
    }
}