using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GPS.Service.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryOutagesController : Controller
    {
        private readonly Parser _parser;

        private readonly List<Outage> _allOutages;

        public QueryOutagesController()
        {
            _parser = new Parser("SOF\\current.sof");
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