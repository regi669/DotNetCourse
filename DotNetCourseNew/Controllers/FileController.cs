using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace DotNetCourseNew.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController : ControllerBase
    {
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            
            var filePath = $"{rootPath}/PrivateFiles/{fileName}";

            var fileExist = System.IO.File.Exists(filePath);

            if (!fileExist) return NotFound($"File {fileName} does not exist");

            var fileContent = System.IO.File.ReadAllBytes(filePath);
            
            var contentProvider = new FileExtensionContentTypeProvider();
            
            contentProvider.TryGetContentType(filePath, out string contentType);
            
            return File(fileContent, contentType, fileName);
        }
    }
}