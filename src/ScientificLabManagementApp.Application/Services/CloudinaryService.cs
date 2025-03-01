using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ScientificLabManagementApp.Application;

namespace ScientificLabManagementApp;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    public CloudinaryService(IConfiguration configManager)
    {
        var account = new Account(configManager["ClOUDINARY_NAME"], configManager["ClOUDINARY_API_KEY"], configManager["ClOUDINARY_API_SECRET"]);
        _cloudinary = new Cloudinary(account);
    }

    private async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            Transformation = new Transformation().Crop("fill").Gravity("face")
            //.Width(500).Height(500)
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.SecureUrl.AbsoluteUri;
    }

    public async Task<string> GetUrlOfUploadedImage(IFormFile image)
    {
        var stream = image.OpenReadStream();
        string extension = Path.GetExtension(image.FileName);
        string newFileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{DateTime.Now.ToFileTime()}.{extension}";
        return await UploadImageAsync(stream, newFileName);
    }
}
