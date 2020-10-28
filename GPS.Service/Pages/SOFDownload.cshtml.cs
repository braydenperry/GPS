using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GPS.WebApp.Pages
{
	public class SOFDownloadModel : PageModel
	{
		private readonly ILogger<SOFDownloadModel> _logger;

		public SOFDownloadModel(ILogger<SOFDownloadModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{
	
		}
	}
}
