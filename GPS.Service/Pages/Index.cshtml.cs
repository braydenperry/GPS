using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GPS.Data;

namespace GPS.WebApp.Pages
{
    public class IndexModel : PageModel
	{
		private readonly IOutageRepository _outageRepository;
		public List<Outage> allOutages;

		public IndexModel(IOutageRepository outageRepository)
		{
			_outageRepository = outageRepository;
			allOutages = _outageRepository.Get();
		}
	}
}
