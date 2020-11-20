using System;
using System.Collections.Generic;
using System.IO;

namespace GPS.Data
{
    public interface IOutageRepository
    {
        public IEnumerable<Outage> Get();

        public IEnumerable<Outage> Get(string tagName);
        /// <summary>
        /// Will return a queried allOutages list based off the dates that are given
        /// Meant to work with a dateRangePicker on the front end which return values such as: "01/01/1987 - 12/31/2020"
        /// </summary>
        /// <param name="StartDateMinMax"></param>
        /// <param name="EndDateMinMax"></param>
        /// <returns></returns>
        public IEnumerable<Outage> Get(string? startDateMinMax, string? endDateMinMax);

        public void Upload(Stream stream);

        public void Delete();
    }
}
