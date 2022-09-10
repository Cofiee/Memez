namespace Memez.Images
{
    public class BaseImageManager : IImageManager
    {
        private readonly string _storagePath;
        public BaseImageManager(string storagePath)
        {
            this._storagePath = storagePath;
        }

        public string Save(IFormFile formFile, int memeId)
        {
            string extension = System.IO.Path.GetExtension(formFile.FileName);
            string imagePath = Path.Combine(_storagePath, (memeId.ToString() + extension));
            using (System.IO.FileStream stream = System.IO.File.Create(imagePath))
            {
                formFile.CopyToAsync(stream);
            }
            return imagePath;
        }
    }
}
