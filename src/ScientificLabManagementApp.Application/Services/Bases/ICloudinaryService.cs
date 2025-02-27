namespace ScientificLabManagementApp.Application;

public interface ICloudinaryService
{
    Task<string?> GetUrlOfUploadedImage(IFormFile? image);
}

