using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Assignment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                // if file is present into the Http Request
                if (file.Length > 0)
                {
                    // Set the Folder Path
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "Documents");

                    // REad the File Name, File Type (ContentDisposition) and also an extension
                    var fileName = ContentDispositionHeaderValue.Parse(
                        file.ContentDisposition).FileName.Trim('"');

                    // Set the Path for writing the file
                    var filePath = Path.Combine(folder, fileName);
                    // Upload the File
                    using (FileStream fs = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await file.CopyToAsync(fs);
                    }
                    return Ok("File is Uploaded Successfully");
                }
                else
                {
                    return BadRequest("File is eith Empty or Damaged");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"File Upload Failed {ex.Message}");
            }
        }
        [HttpGet("Download/{FileName}")]
        public async Task<IActionResult> Download(string FileName)
        {
            try
            {
                // if file is present into the Http Request
                if (string.IsNullOrEmpty(FileName))
                    return BadRequest("File Name must be required");

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
                var filePath = Path.Combine(folder, FileName);

                if (!System.IO.File.Exists(filePath))
                    return BadRequest($"{FileName} File name does not exists!");

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, contentType, Path.GetFileName(filePath));

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"File Download Failed {ex.Message}");
            }
        }
    }
}
