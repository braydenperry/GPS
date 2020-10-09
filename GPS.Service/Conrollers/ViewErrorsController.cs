using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GPS.Service.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewErrorsController : ControllerBase
    {
        private readonly Parser _parser;
        private readonly List<string> _allErrors;

        public ViewErrorsController()
        {
            _parser = new Parser("SOF\\current.sof");
            _allErrors = _parser.ErrorLog;
            var tags = new { tags = _allErrors };

            Console.WriteLine(JsonConvert.SerializeObject(tags));
        }

        [HttpGet]
        public IActionResult Get()
        {
            //TODO: Get this line of code working.
            return (IActionResult)_allErrors;
        }
    }
}
