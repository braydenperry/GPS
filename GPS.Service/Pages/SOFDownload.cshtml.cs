using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GPS.Data;

namespace GPS.WebApp.Pages
{
	public class SOFDownloadModel : PageModel
	{
		private readonly ILogger<SOFDownloadModel> _logger;

		public readonly Parser sofParser;
		public List<Outage> allOutages;

		public SOFDownloadModel(ILogger<SOFDownloadModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
	
		}
	}
}
