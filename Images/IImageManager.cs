namespace Memez.Images
{
    public interface IImageManager
    {
        public string Save(IFormFile formFile, int memeId);
    }
}
