using Microsoft.AspNetCore.Http;

namespace _01_Framework.Application
{
	public interface IFileUpload
	{
		string Upload(IFormFile file, string path);
	}
}