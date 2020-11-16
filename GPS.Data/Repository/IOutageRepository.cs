using System.Collections.Generic;
using System.IO;

namespace GPS.Data
{
    public interface IOutageRepository
    {
        public IEnumerable<Outage> Get();

        public IEnumerable<Outage> Get(string tagName);

        public void Upload(Stream stream);

        public void Delete();
    }
}
