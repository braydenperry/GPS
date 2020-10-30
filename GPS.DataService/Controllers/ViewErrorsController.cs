using System.Collections.Generic;
using GPS.Data;
using Microsoft.AspNetCore.Mvc;

namespace GPS.DataService.Controllers
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
            _parser = new Parser();
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
