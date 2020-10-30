using System;
using System.Collections.Generic;
using System.Linq;
using GPS.Data;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (_outageRepository.Get() == null)
                {
                    throw new ArgumentException("There were no outages: Possible FileNotFound Exception");
                }
                else
                {
                    return Json(_outageRepository.Get());
                }
            }catch(Exception ex)
            {
                return StatusCode(404);
            }
        }

        [HttpGet("{tagName}")]
        public IActionResult Get(string tagName)
        {
            return Json(_outageRepository.Get().Where(o => o.TagName == tagName.ToUpper()));
        }
    }
}
