﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GPS.Data
{
    public class OutageRepository : IOutageRepository
    {
        private readonly Parser _parser;

        private IEnumerable<Outage> AllOutages;

        private readonly object SOFFileLock = new object();

        private readonly string _solutionDirectory;

        private readonly string _sofPath;

        public OutageRepository()
        {

            lock (SOFFileLock)
            {
                _parser = new Parser();
                AllOutages = _parser.PopulateObjectsFromSof();
                _solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                _sofPath = Path.Combine(_solutionDirectory, "GPS.Data\\SOF\\current.sof");
            }

            if (_parser.Outages != null)
            {
                AllOutages = _parser.PopulateObjectsFromSof();

            }
            else
            {
                //Send an error code because a file could not be found to pull outage data from
            }
        }

        public void Delete()
        {

            if (File.Exists(_sofPath))
            {

                lock (SOFFileLock)
                {
                    File.Delete(_sofPath);
                    AllOutages = null;
                }

            }
            else
            {
                throw new Exception();
            }

        }

        public IEnumerable<Outage> Get()
        {

            if (AllOutages != null)
            {

                lock (SOFFileLock)
                {
                    return AllOutages;
                }

            }
            else
            {
                throw new Exception();
            }

        }

        public IEnumerable<Outage> Get(string tagName)
        {

            if (AllOutages != null)
            {

                lock (SOFFileLock)
                {
                    return AllOutages.Where(o => o.TagName == tagName.ToUpper());
                }

            }
            else
            {
                throw new Exception();
            }

        }

        public void Upload(Stream stream)
        {

            try
            {
                Parser parser = new Parser(stream);
                AllOutages = parser.PopulateObjectsFromSof();

                var fileStream = File.Create(_sofPath);

                lock (SOFFileLock)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }

                fileStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
