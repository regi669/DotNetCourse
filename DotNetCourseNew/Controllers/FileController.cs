using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace DotNetCourseNew.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController : ControllerBase
    {
        [HttpGet]
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

        [HttpPost]
        public ActionResult Upload([FromForm] IFormFile file)
        {
            if (file is not null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
            
                var filePath = $"{rootPath}/PrivateFiles/{file.FileName}";

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}