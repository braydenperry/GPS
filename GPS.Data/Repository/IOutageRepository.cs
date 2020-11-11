using GPS.Data.ParserObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
