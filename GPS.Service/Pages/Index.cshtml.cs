using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GPS.Data;

namespace GPS.WebApp.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public readonly Parser sofParser;
		public List<Outage> allOutages;

		public IndexModel(ILogger<IndexModel> logger)
		{
			sofParser = new Parser("SOF\\validTest.sof");
			allOutages = sofParser.PopulateObjectsFromSof();
			_logger = logger;
		}

		public void OnGet()
		{
			foreach (var outages in
			from Outage outages in allOutages
			select outages)
			{
				Console.WriteLine(outages.SatelliteVehicleId);
			}
		}
	}
}
