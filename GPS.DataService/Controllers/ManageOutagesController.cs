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
    public class ManageOutagesController : ControllerBase
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
        public string Post(IFormFile file)
        {

            try
            {
                _outageRepository.Upload(file);
                return "File uploaded";
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
                _outageRepository.Delete();
                return "File deleted";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
        #endregion
    }
}
