using GPS.Data.ParserObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Data
{
    public interface IOutageRepository
    {
        public List<Outage> Get();

        public void Upload(IFormFile file);

        public void Delete();
    }
}
