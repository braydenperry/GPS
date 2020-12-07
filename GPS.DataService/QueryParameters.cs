using Microsoft.AspNetCore.Mvc;

namespace GPS.DataService
{
    [BindProperties]
    public class QueryParameters
    {     
        public string TagName { get; set; }

        public string StartDateMinMax { get; set; }

        public string EndDateMinMax { get; set; }
    }
}
