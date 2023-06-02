using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.ImageUploadModule;

public class ImageUploadModule : IModule<IAuthorizationService, IImageStorageService>
{
    public ApiEndpoint[] AddModule(IAuthorizationService dependency1, IImageStorageService dependency2)
    {
        return new[]{
            new ApiEndpoint("/products/{productId}/images", (Guid productId, IFormFile imageStream, [FromServices] IImageRepository imageRepo, ClaimsPrincipal user) => new UploadImage(imageRepo, dependency1, dependency2).ExecuteAsync(productId, imageStream.OpenReadStream(), user), HttpMethod.Post)
        };
    }
}