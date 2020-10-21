using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GPS.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageOutagesController : ControllerBase
    {
        #region Properties
        public static IWebHostEnvironment _environment;

        // File path for the current.sof file.
        private readonly string _filePath;
        #endregion

        #region Constructor
        public ManageOutagesController(IWebHostEnvironment environment)
        {
            _environment = environment;
            _filePath = _environment.WebRootPath + "\\SOF\\current.sof";
        }
        #endregion

        #region HTTP Methods
        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload file)
        {

            try
            {

                if (file.File.Length > 0) // Make sure there's actually a file being uploaded.
                {
                    // Get file path for new file being uploaded.
                    var newFilePath = _environment.WebRootPath + "\\SOF\\" + file.File.FileName;

                    if (ValidExtension(newFilePath)) // Make sure new file being uploaded has .sof extension.
                    {
                        // Create or overwrite a file at the secified path.  
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
                    return "No file provided.";
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

                if (SOFExists()) // Make sure the SOF file exists.
                {
                    // If it does, delete it.
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
        #endregion

        #region Validation Methods
        /// <summary>
        /// Checks if the SOF file exists in the static resources of the app.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if the extension of a given file path is .sof. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool ValidExtension(string filePath)
        {
            // Get the extesnion of the provided file path.
            string extension = Path.GetExtension(filePath);

            if (extension.ToLower() == ".sof") // Make sure the extension is .sof.
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
    }
}
