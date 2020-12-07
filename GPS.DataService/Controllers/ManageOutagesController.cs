using GPS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GPS.DataService.Controllers
{
    [Route("v1/outages")]
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
                return StatusCode(500);
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
                return StatusCode(500);
            }
        }
        #endregion
    }
}
