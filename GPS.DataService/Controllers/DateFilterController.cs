﻿using System;
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

        [HttpGet("{StartDateMinMax?}/{EndDateMinMax?}")]
        public IActionResult Get(string startDateMinMax = null, string endDateMinMax = null)
        {
            try
            {
                if (_outageRepository.Get() == null)
                {
                    return StatusCode(404);
                }

                return Json(_outageRepository.Get(startDateMinMax, endDateMinMax));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
        #endregion
    }
}
