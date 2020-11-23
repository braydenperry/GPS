using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;


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

            _parser = new Parser();
            _solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            _sofPath = Path.Combine(_solutionDirectory, "GPS.Data\\SOF\\current.sof");

            if (_parser.Outages != null)
            {

                lock (SOFFileLock)
                {
                    AllOutages = _parser.PopulateObjectsFromSof();
                }

            }
            else
            {
                throw new Exception();
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

        /// <summary>
        /// Will return a queried allOutages list based off the dates that are given
        /// Meant to work with a dateRangePicker on the front end which return values such as: "01/01/1987 - 12/31/2020"
        /// </summary>
        /// <param name="StartDateMinMax"></param>
        /// <param name="EndDateMinMax"></param>
        /// <returns></returns>
        public IEnumerable<Outage> Get(string startDateMinMax = null, string endDateMinMax = null)
        {
            //Tells the DateTime parser to parse for US time formats
            CultureInfo us = new CultureInfo("en-US");

            if (AllOutages != null)
            {
                //Query based on both a startDate and an endDate as well. Return only those that are in both parameters
                if (startDateMinMax != null && endDateMinMax != null)
                {
                    //Get the dates in a place that you can convert them from a string to a DateTime
                    var startDates = startDateMinMax.Split('_');
                    DateTime startMin = DateTime.ParseExact(startDates[0], "MM-dd-yyyy", us);
                    DateTime startMax = DateTime.ParseExact(startDates[1], "MM-dd-yyyy", us);
                    var endDates = endDateMinMax.Split('_');
                    DateTime endMin = DateTime.ParseExact(endDates[0], "MM-dd-yyyy", us);
                    DateTime endMax = DateTime.ParseExact(endDates[1], "MM-dd-yyyy", us);

                    lock (SOFFileLock)
                    {
                        return AllOutages.Where(o => o.StartTime >= startMin && o.StartTime <= startMax &&
                                                     o.EndTime >= endMin     && o.EndTime <= endMax);
                    }
                } //Query based off of startDate
                else if (startDateMinMax != null && endDateMinMax == null)
                {
                    //Get the dates in a place that you can convert them from a string to a DateTime
                    var startDates = startDateMinMax.Split('_');
                    DateTime startMin = DateTime.ParseExact(startDates[0], "MM-dd-yyyy", us);
                    DateTime startMax = DateTime.ParseExact(startDates[1], "MM-dd-yyyy", us);

                    lock (SOFFileLock)
                    {
                        return AllOutages.Where(o => o.StartTime >= startMin && o.StartTime <= startMax);
                    }
                }//Query based off of endDate
                else if (endDateMinMax != null && startDateMinMax == null)
                {
                    //Get the dates in a place that you can convert them from a string to a DateTime
                    var endDates = endDateMinMax.Split('_');
                    DateTime endMin = DateTime.ParseExact(endDates[0], "MM-dd-yyyy", us);
                    DateTime endMax = DateTime.ParseExact(endDates[1], "MM-dd-yyyy", us);

                    lock (SOFFileLock)
                    {
                        return AllOutages.Where(o => o.EndTime >= endMin && o.EndTime <= endMax);
                    }
                }//They are both null, return a list of all outages.
                else
                {
                    lock (SOFFileLock)
                    {
                        return AllOutages;
                    }
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
