using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork_Backend.Models;

namespace SocialNetwork_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FileController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILoggerFactory loggerFactory,
            IHostingEnvironment hostingEnvironment)
            : base(signInManager, userManager, loggerFactory)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("post")]
        [HttpPost]
        public async Task<IActionResult> Post(List<IFormFile> photos)
        {
            long size = photos.Sum(f => f.Length);

            // full path to file in temp location
            var tempFilePath = Path.GetTempFileName();
            var filePath = tempFilePath;
            byte[] photosarray = { };

            List<UploadedFile> uploadedFiles = new List<UploadedFile>();

            foreach (var formFile in photos)
            {
                if (formFile.Length > 0)
                {
                    var uniqueFileName = GetUniqueFileName(formFile.FileName);
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "img");

                    var finalFilePath = Path.Combine(uploads, uniqueFileName);

                    using (var stream = new FileStream(finalFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    uploadedFiles.Add(new UploadedFile() { OriginalName = formFile.FileName, UploadedFileName = uniqueFileName });

                }
            }

            return Ok(uploadedFiles);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
