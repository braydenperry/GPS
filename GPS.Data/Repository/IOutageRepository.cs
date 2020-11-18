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
        /// </summary>
        /// <param name="StartDateMinMax"></param>
        /// <param name="EndDateMinMax"></param>
        /// <returns></returns>
        public IEnumerable<Outage> Get(DateTime? startDateMin, DateTime? startDateMax, DateTime? endDateMin, DateTime? endDateMax);

        public void Upload(Stream stream);

        public void Delete();
    }
}
