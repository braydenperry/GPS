using GPS.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GPS.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryOutagesController : Controller
    {
        #region Properties
        private readonly IOutageRepository _outageRepository;
        #endregion

        #region Constructor
        public QueryOutagesController(IOutageRepository outageRepository)
        {
            _outageRepository = outageRepository;
        }
        #endregion

        #region HTTP Methods
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                if (_outageRepository.Get() == null)
                {
                    return StatusCode(404);
                }
                return Json(_outageRepository.Get());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("{tagName}")]
        public IActionResult Get(string tagName)
        {

            try
            {
                if (_outageRepository.Get() == null)
                {
                    return StatusCode(404);
                }
                return Json(_outageRepository.Get(tagName));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
        #endregion
    }
}
