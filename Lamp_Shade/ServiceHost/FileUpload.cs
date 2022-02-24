using _0_Framework.Application;
using _01_Framework.Application;
using System;



namespace ServiceHost
{
	public class FileUpload : IFileUpload
	{
		private readonly IWebHostEnvironment _webHostEnviroment;

		public FileUpload(IWebHostEnvironment webHostEnviroment)
		{
			_webHostEnviroment = webHostEnviroment;
		}

		public string Upload(IFormFile file,string path)
		{
			if(path == null)
				return "";

			var directoryPath = $"{_webHostEnviroment.WebRootPath}/UploadedFiles/{path}";
			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);
			var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
			var filePath = $"{directoryPath}/{fileName}";
			using var output = File.Create(filePath);
			file.CopyTo(output);
			
			return $"/UploadedFiles/{path}/{fileName}";
		}
	}
}