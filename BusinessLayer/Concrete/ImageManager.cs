using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace BusinessLayer.Concrete;

public class ImageManager
{
    private IImageDal _imageDal;

    public ImageManager(IImageDal imageDal)
    {
        _imageDal = imageDal;
    }

    public string SaveFileToFolder(IFormFile file)
    {

        var folderPath = Path.Combine(Environment.CurrentDirectory, @"./wwwroot/images");
            
        var path = Path.GetFullPath(folderPath);

        var randomNameWithExtension = RandomString(Path.GetExtension(file.FileName));

        using (FileStream stream = new FileStream(Path.Combine(path, randomNameWithExtension), FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return Path.Combine("/images", randomNameWithExtension).Replace('\\', '/');

    }

    public void RemoveFile(string imagePath)
    {
        var imageFullPath = Path.Combine(Environment.CurrentDirectory, $@"./wwwroot{imagePath}");
        var path = Path.GetFullPath(imageFullPath);

        if (File.Exists(path))
            File.Delete(path);
        else
        {
            var errorMessage = $@"image full path: {imageFullPath}, path: {path}";
            throw new FileNotFoundException(errorMessage);
        }
    }

    private string RandomString(string fileExtension)
    {
        var randomString = Guid.NewGuid().ToString("N").Substring(0, 15) + fileExtension;
        return randomString;
    }


        
}