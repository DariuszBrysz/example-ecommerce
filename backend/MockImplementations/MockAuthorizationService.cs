using Backend.Model;

namespace Backend.MockImplementations;

public class MockAuthorizationService
{
    public void Authorize(Permission permission)
    {
        switch (permission)
        {
            case Permission.GetProduct:
                break;
            case Permission.ListProducts:
                break;
            case Permission.ListProductsWithFilters:
                break;
            case Permission.CreateProduct:
                break;
            case Permission.UpdateProduct:
                break;
            case Permission.AddStock:
                break;
            case Permission.RemoveStock:
                break;
            case Permission.UploadImage:
                break;
            default:
                throw new NotImplementedException();
        }
    }
}