namespace P013KatmanliBlog.WebAPIUsing.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "/Img/")
        {
            string fileName = "";

            fileName = formFile.FileName;
            string directory = Directory.GetCurrentDirectory() + "/wwwroot" + filePath + fileName;
            using var stream = new FileStream(directory, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return fileName;
        }
        public static bool FileRemove(string fileName, string filePath = "/wwwroot/Img/")
        {
            string directory = Directory.GetCurrentDirectory() + filePath + fileName;
            if (File.Exists(directory)) 
            {
                File.Delete(directory); 
                return true; 
            }
            return false; 
        }
    }
}
