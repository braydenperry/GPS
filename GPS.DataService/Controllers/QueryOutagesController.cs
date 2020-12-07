using GPS.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GPS.DataService.Controllers
{
    [Route("v1/outages")]
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
        public IActionResult Get([FromQuery] QueryParameters parameters)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (parameters.TagName != null)
                    {
                        return Json(_outageRepository.Get(parameters.TagName));
                    }
                    
                    if (parameters.StartDateMinMax != null || parameters.EndDateMinMax != null)
                    {
                        return Json(_outageRepository.Get(parameters.StartDateMinMax, parameters.EndDateMinMax));
                    }

                    return Json(_outageRepository.Get());
                }

                return StatusCode(400);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}
