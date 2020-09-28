using GPS.Data.ParserObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GPS.Data
{
    public class Parser
    {
        private readonly XmlSerializer Serializer;

        public GpsIsFile Outages { get; set; }

        Validate validateObj = new Validate();

        public Parser(string filePath)
        {
            try
            {
                //Serializes the .sof and populates each of the classes
                Serializer = new XmlSerializer(typeof(GpsIsFile));
                using Stream reader = new FileStream(filePath, FileMode.Open);
                Outages = (GpsIsFile)Serializer.Deserialize(reader);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("FileNotFoundException");
                //Call error page form that Tanner will make :)
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// Takes a .sof and populates the classes with the information included therein
        /// </summary>
        /// <returns>
        /// A list of all outages consolidated in one place for ease of use when querying
        /// </returns>
        public List<Outage> PopulateObjectsFromSof()
        {
            //Populate the AllOutages List
            List<Outage> allOutages = new List<Outage>();
            foreach (Historical historicalOutage in Outages.HistoricalOutages)
            {
                bool valid = validateObj.ValidateHistorical(historicalOutage);
                if (!valid)
                    continue;
                    //TODO: This is where the error log message will go once we figure out what that's all about.

                allOutages.Add(new Outage
                {
                    TagName = "HISTORICAL",
                    SatelliteVehicleId = historicalOutage.SatelliteVehicleId,
                    SatelliteVehicleNumber = historicalOutage.SatelliteVehicleNumber,
                    Name = historicalOutage.Name,
                    Type = historicalOutage.Type,
                    Reference = historicalOutage.Reference,
                    StartTime = GpsIsFile.ToDateTime(historicalOutage.StartYear, historicalOutage.StartDayOfYear, historicalOutage.StartHour, historicalOutage.StartMinute, historicalOutage.StartSecond),
                    EndTime = GpsIsFile.ToDateTime(historicalOutage.EndYear, historicalOutage.EndDayOfYear, historicalOutage.EndHour, historicalOutage.EndMinute, historicalOutage.EndSecond)
                });
            }

            foreach (Current currentOutage in Outages.CurrentOutages)
            {
                bool valid = validateObj.ValidateCurrent(currentOutage);
                if (!valid)
                    continue;
                    //TODO: This is where the error log message will go once we figure out what that's all about.

                allOutages.Add(new Outage
                {
                    TagName = "CURRENT",
                    SatelliteVehicleId = currentOutage.SatelliteVehicleId,
                    SatelliteVehicleNumber = currentOutage.SatelliteVehicleNumber,
                    Name = currentOutage.Name,
                    Type = currentOutage.Type,
                    Reference = currentOutage.Reference,
                    StartTime = GpsIsFile.ToDateTime(currentOutage.StartYear, currentOutage.StartDayOfYear, currentOutage.StartHour, currentOutage.StartMinute, currentOutage.StartSecond)
                });
            }

            foreach (Predicted predictedOutage in Outages.PredictedOutages)
            {
                bool valid = validateObj.ValidatePredicted(predictedOutage);
                if (!valid)
                    continue;
                    //TODO: This is where the error log message will go once we figure out what that's all about.

                allOutages.Add(new Outage
                {
                    TagName = "PREDICTED",
                    SatelliteVehicleId = predictedOutage.SatelliteVehicleId,
                    SatelliteVehicleNumber = predictedOutage.SatelliteVehicleNumber,
                    Name = predictedOutage.Name,
                    Type = predictedOutage.Type,
                    Reference = predictedOutage.Reference,
                    StartTime = GpsIsFile.ToDateTime(predictedOutage.StartYear, predictedOutage.StartDayOfYear, predictedOutage.StartHour, predictedOutage.StartMinute, predictedOutage.StartSecond)
                });
            }

            return allOutages;
        }
    }
}
