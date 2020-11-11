using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using GPS.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPS.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageOutagesController : Controller
    {
        #region Properties
        public readonly IOutageRepository _outageRepository;
        #endregion

        #region Constructor
        public ManageOutagesController(IOutageRepository outageRepository)
        {
            _outageRepository = outageRepository;
        }
        #endregion

        #region HTTP Methods
        [HttpPost]
        public IActionResult Post(IFormFile file)
        {

            try
            {

                if (file != null)
                {
                    _outageRepository.Upload(file.OpenReadStream());
                    return StatusCode(202, "File Uploaded");
                }
                else
                {
                    throw new ArgumentNullException();
                }
                
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

        }

        [HttpDelete]
        public IActionResult Delete()
        {

            try
            {
                _outageRepository.Delete();
                return StatusCode(202, "File Deleted");
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

        }
        #endregion
    }
}
