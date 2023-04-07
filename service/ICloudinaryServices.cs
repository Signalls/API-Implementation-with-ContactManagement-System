namespace ContactManager.service
{
    public interface ICloudinaryServices
    {
        Task<Dictionary<string, string>> UploadAsync(IFormFile file);
    }
}
