using System;
using System.IO;
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

        private readonly string _filePath;

        public ManageOutagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
            _filePath = _environment.WebRootPath + "\\SOF\\current.sof";
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
                    var newFilePath = _environment.WebRootPath + "\\SOF\\" + file.File.FileName;

                    if (SOFExists())
                    {
                        Delete();
                    }
                    
                    if (ValidExtension(newFilePath))
                    {
                        using FileStream fileStream = System.IO.File.Create(newFilePath);
                        await file.File.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return "File uploaded.";
                    }
                    else
                    {
                        return "Invalid file type.";
                    }
    
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

        [HttpDelete]
        public string Delete()
        {

            try
            {

                if (SOFExists())
                {
                    System.IO.File.Delete(_filePath);
                }
                else
                {
                    return "SOF file does not exist.";
                }

                return "Delete successful";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            
        }

        private bool SOFExists()
        {

            if (System.IO.File.Exists(_filePath))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool ValidExtension(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            
            if (extension.ToLower() == ".sof")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}