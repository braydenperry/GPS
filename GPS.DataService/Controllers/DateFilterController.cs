using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPS.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateFilterController : Controller
    {
        #region Properties
        private readonly IOutageRepository _outageRepository;
        #endregion

        #region Constructor
        public DateFilterController(IOutageRepository outageRepository)
        {
            _outageRepository = outageRepository;
        }
        #endregion
        #region HTTP Methods

        [HttpGet]
        public IActionResult Get()
        {
            //If there is no filter, return a list of all outages
            try
            {
                return Json(_outageRepository.Get());
            }
            catch (Exception)
            {
                return StatusCode(404);
            }

        }

        [HttpGet("{StartDateMinMax}/{EndDateMinMax}")]
        public IActionResult Get(string? startDateMinMax, string? endDateMinMax)
        {
            try
            {
                return Json(_outageRepository.Get(startDateMinMax, endDateMinMax));
            }
            catch (Exception)
            {
                return StatusCode(404);
            }

        }
        #endregion
    }
}
