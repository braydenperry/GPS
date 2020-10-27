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

        public OutageRepository()
        {
            _parser = new Parser();
            _allOutages = _parser.PopulateObjectsFromSof();
        }

        public void Delete()
        {
            
            lock (SOFFileLock) { 

                if (File.Exists("\\SOF\\current.sof")) // Make sure the SOF file exists.
                {
                    // If it does, delete it.
                    File.Delete("\\SOF\\current.sof");
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
                    // Get file path for new file being uploaded.
                    string executionFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    string sofPath = Path.Combine(executionFolder, "SOF/current.sof");

                    if (ValidExtension(sofPath)) // Make sure new file being uploaded has .sof extension.
                    {
                        // Create or overwrite a file at the secified path.  
                        using FileStream fileStream = File.Create(sofPath);
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
