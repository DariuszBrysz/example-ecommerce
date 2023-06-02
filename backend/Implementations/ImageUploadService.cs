using Azure.Storage.Blobs;
using Backend.ImageUploadModule;

namespace Backend.Implementations;

public class ImageUploadService : IImageStorageService
{
    private readonly IConfiguration _configuration;
    private readonly string _blobConnectionString;
    private readonly string _blobUrl;
    private readonly string _containerName;

    public ImageUploadService(IConfiguration configuration)
    {
        _configuration = configuration;
        _blobConnectionString = _configuration.GetConnectionString("BlobConnectionString") ?? "";
        _blobUrl = _configuration.GetValue<string>("Images:RootUrl") ?? "";
        _containerName = _configuration.GetValue<string>("Images:ContainerName") ?? "";
    }

    public async Task<string> UploadImageAsync(Guid imageId, Stream imageStream)
    {
        string fileName = Guid.NewGuid().ToString() + ".jpg";

        BlobContainerClient container = new BlobContainerClient(_blobConnectionString, _containerName);
        BlobClient blobClient = container.GetBlobClient(fileName);
        await blobClient.UploadAsync(imageStream, true);

        return _blobUrl + _containerName + "/" + fileName;
    }
}