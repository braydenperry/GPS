using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPS.Service.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageOutagesController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public ManageOutagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        
        public class FileUpload
        {
            public IFormFile File { get; set; }
        }
        
        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload file)
        {
            try
            {
                if (file.File.Length > 0) // Make sure there's actually a file
                {
                    using FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\SOF\\" + file.File.FileName);
                    await file.File.CopyToAsync(fileStream);
                    fileStream.Flush();
                    return "File uploaded.";
                }
                else
                {
                    return "File failed to upload.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}