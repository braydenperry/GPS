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
        private readonly Parser _parser;

        private readonly List<Outage> _allOutages;

        public QueryOutagesController()
        {
            _parser = new Parser();
            _allOutages = _parser.PopulateObjectsFromSof();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(_allOutages);
        }

        [HttpGet("{tagName}")]
        public IActionResult Get(string tagName)
        {
            return Json(_allOutages.Where(o => o.TagName == tagName.ToUpper()));
        }
    }
}
