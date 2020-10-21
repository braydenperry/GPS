using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.Data
{
    public interface IOutageRepository
    {
        public List<Outage> Get();

        public void Upload(FileUpload file);

        public void Delete();
    }
}
