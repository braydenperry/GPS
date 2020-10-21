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
        public readonly IOutageRepository _outageRepository;
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
            return Json(_outageRepository.Get());
        }

        [HttpGet("{tagName}")]
        public IActionResult Get(string tagName)
        {
            return Json(_outageRepository.Get().Where(o => o.TagName == tagName.ToUpper()));
        }
    }
}
