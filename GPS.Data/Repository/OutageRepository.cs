using GPS.Data.ParserObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Data
{
    public class OutageRepository : IOutageRepository
    {
        private readonly Parser _parser;

        private readonly List<Outage> _allOutages;

        private readonly object SOFFileLock = new object();

        private readonly string _solutionDirectory;

        private readonly string _sofPath;

        public OutageRepository()
        {
            
            lock (SOFFileLock){
                _parser = new Parser();
                _allOutages = _parser.PopulateObjectsFromSof();
                _solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                _sofPath = Path.Combine(_solutionDirectory, "GPS.Data\\SOF\\current.sof");
            }

            if (_parser.Outages != null)
            {
                    _allOutages = _parser.PopulateObjectsFromSof();

            }
            else
            {
                //Send an error code because a file could not be found to pull outage data from
            }
        }

        public void Delete()
        {
            
            lock (SOFFileLock) { 

                if (File.Exists(_sofPath)) // Make sure the SOF file exists.
                {
                    // If it does, delete it.
                    File.Delete(_sofPath);
                }

            }

        }

        public List<Outage> Get()
        {

            lock (SOFFileLock)
            {
                return _allOutages;
            }

        }

        public void Upload(IFormFile file)
        {

            lock (SOFFileLock)
            {

                if (file.Length > 0) // Make sure there's actually a file being uploaded.
                {

                    if (ValidExtension(_sofPath)) // Make sure new file being uploaded has .sof extension.
                    {
                        // Create or overwrite a file at the secified path.  
                        using FileStream fileStream = File.Create(_sofPath);
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                }

            }

        }

        /// <summary>
        /// Checks if the extension of a given file path is .sof. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool ValidExtension(string filePath)
        {
            // Get the extesnion of the provided file path.
            string extension = Path.GetExtension(filePath);

            if (extension.ToLower() == ".sof") // Make sure the extension is .sof.
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
