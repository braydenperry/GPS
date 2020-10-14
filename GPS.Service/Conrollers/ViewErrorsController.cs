using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
        private readonly string retVal;

        public ViewErrorsController()
        {
            _parser = new Parser("SOF\\current.sof");
            _parser.PopulateObjectsFromSof();
            _allErrors = _parser.ErrorLog;

            foreach (var error in _allErrors)
            {
                retVal += error + "\n";
            }

        }

        [HttpGet]
        public string Get()
        {
            //TODO: Get this line of code working.
            return retVal;
        }
    }
}
