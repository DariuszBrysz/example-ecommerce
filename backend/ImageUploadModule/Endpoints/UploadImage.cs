using System.Security.Claims;

namespace Backend.ImageUploadModule
{
    public class UploadImage
    {
        private readonly IImageRepository _imageRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IImageStorageService _imageStorageService;

        public UploadImage(IImageRepository imageRepository, IAuthorizationService authorizationService, IImageStorageService imageStorageService)
        {
            _imageRepository = imageRepository;
            _authorizationService = authorizationService;
            _imageStorageService = imageStorageService;
        }

        public async Task<Image> ExecuteAsync(Guid productId, Stream imageStream, ClaimsPrincipal user)
        {
            _authorizationService.Authorize(Permissions.UploadImage, user);
            Guid imageId = Guid.NewGuid();
            string relativeUrl = await _imageStorageService.UploadImageAsync(imageId, imageStream);
            return await _imageRepository.AddImageAsync(new Image
            {
                Id = imageId,
                ProductId = productId,
                Url = relativeUrl
            });
        }
    }
}