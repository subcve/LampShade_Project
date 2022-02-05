using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public interface IFileUpload
    {
        string Upload(IFormFile file,string path);
    }
}