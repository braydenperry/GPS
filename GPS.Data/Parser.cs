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

        public Parser(string filePath)
        {
            //Serializes the .sof and populates each of the classes
            Serializer = new XmlSerializer(typeof(GpsIsFile));
            using Stream reader = new FileStream(filePath, FileMode.Open);
            Outages = (GpsIsFile)Serializer.Deserialize(reader);
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
                //Add validation here!

                allOutages.Add(new Outage
                {
                    TagName = "HISTORICAL",
                    SatelliteVehicleId = historicalOutage.SatelliteVehicleId,
                    SatelliteVehicleNumber = historicalOutage.SatelliteVehicleNumber,
                    Name = historicalOutage.Name,
                    Type = historicalOutage.Type,
                    Reference = historicalOutage.Reference,
                    StartYear = historicalOutage.StartYear,
                    StartDayOfYear = historicalOutage.StartDayOfYear,
                    StartHour = historicalOutage.StartHour,
                    StartMinute = historicalOutage.StartMinute,
                    StartSecond = historicalOutage.StartSecond,
                    EndYear = historicalOutage.EndYear,
                    EndDayOfYear = historicalOutage.EndDayOfYear,
                    EndHour = historicalOutage.EndHour,
                    EndMinute = historicalOutage.EndMinute,
                    EndSecond = historicalOutage.EndSecond
                });
            }

            foreach (Current currentOutage in Outages.CurrentOutages)
            {
                //Add validation here!

                allOutages.Add(new Outage
                {
                    TagName = "CURRENT",
                    SatelliteVehicleId = currentOutage.SatelliteVehicleId,
                    SatelliteVehicleNumber = currentOutage.SatelliteVehicleNumber,
                    Name = currentOutage.Name,
                    Type = currentOutage.Type,
                    Reference = currentOutage.Reference,
                    StartYear = currentOutage.StartYear,
                    StartDayOfYear = currentOutage.StartDayOfYear,
                    StartHour = currentOutage.StartHour,
                    StartMinute = currentOutage.StartMinute,
                    StartSecond = currentOutage.StartSecond
                });
            }

            foreach (Predicted predictedOutage in Outages.PredictedOutages)
            {
                //Add validation here!

                allOutages.Add(new Outage
                {
                    TagName = "PREDICTED",
                    SatelliteVehicleId = predictedOutage.SatelliteVehicleId,
                    SatelliteVehicleNumber = predictedOutage.SatelliteVehicleNumber,
                    Name = predictedOutage.Name,
                    Type = predictedOutage.Type,
                    Reference = predictedOutage.Reference,
                    StartYear = predictedOutage.StartYear,
                    StartDayOfYear = predictedOutage.StartDayOfYear,
                    StartHour = predictedOutage.StartHour,
                    StartMinute = predictedOutage.StartMinute,
                    StartSecond = predictedOutage.StartSecond
                });
            }

            return allOutages;
        }
    }
}
