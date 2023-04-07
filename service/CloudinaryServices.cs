using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ContactManager.service
{


    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly IConfiguration _configuration;

        public CloudinaryServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }






        public async Task<Dictionary<string, string>> UploadAsync(IFormFile file)
        {




            var account = new Account(
            "df8epabln",
            "113637731888417",
            "9Y3_ww39PvMk1Dwg1uA-6b9k5Kk"
            );

            var cloudinary = new Cloudinary(account);




            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
                };
                var Result = await cloudinary.UploadAsync(uploadParams);

                var result = new Dictionary<string, string>();
                result.Add("PublicId", Result.PublicId);
                result.Add("Url", Result.Url.ToString());
                return result;

            }
        }






























        //public async Task<Dictionary<string, string>> UploadFileAsync(IFormFile file)
        //{
        //    var cloudinaryAccount = new Account
        //    {
        //        ApiKey = _configuration.GetSection("CloudinarySettings:ApiKey").Value,
        //        ApiSecret = _configuration.GetSection("CloudinarySettings:ApiSecret").Value,
        //        Cloud = _configuration.GetSection("CloudinarySettings:CloudName").Value
        //    };


        //    var cloudinary = new Cloudinary(cloudinaryAccount);

        //    if (file.Length > 0 && file.Length <= (1024 * 1024 * 2))
        //    {
        //        if (file.ContentType.Equals("image/png") || file.ContentType.Equals("image/jpeg") || file.ContentType.Equals("image/jpg"))
        //        {
        //            UploadResult uploadResult = new ImageUploadResult();
        //            using (var stream = file.OpenReadStream())
        //            {
        //                var uploadParameters = new ImageUploadParams
        //                {
        //                    File = new FileDescription(file.FileName, stream),
        //                    Transformation = new Transformation().Width(300).Height(300).Crop("Fill").Gravity("Face")
        //                };

        //                uploadResult = await cloudinary.UploadAsync(uploadParameters);
        //            }
        //            var result = new Dictionary<string, string>();
        //            result.Add("PublicId", uploadResult.PublicId);
        //            result.Add("Url", uploadResult.Url.ToString());
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }


        //    }
        //    else
        //    {
        //        return null;
        //    }




    }
}

