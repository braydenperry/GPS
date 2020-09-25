using GPS.Data.ParserObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS.Data
{
    public class Validate
    {
        public bool ValidateHistorical(Historical outage)
        {
            //If they are supposed to be numbers, check that
            if (!(isNumeric(outage.Reference)
                && isNumeric(outage.SatelliteVehicleId)
                && isNumeric(outage.SatelliteVehicleNumber)
                && isNumeric(outage.StartYear)
                && isNumeric(outage.StartDayOfYear)
                && isNumeric(outage.StartHour)
                && isNumeric(outage.StartMinute)
                && isNumeric(outage.StartSecond)
                && isNumeric(outage.EndYear)
                && isNumeric(outage.EndDayOfYear)
                && isNumeric(outage.EndHour)
                && isNumeric(outage.EndMinute)
                && isNumeric(outage.EndSecond)
                ))
            {
                //TODO: Log error
                return false;
            }
            //If they are NOT supposed to be numbers, check that
            if (outage.Name.Any(char.IsDigit) || outage.Type.Any(char.IsDigit))
            {
                //TODO: Log error
                return false;
            }
            //If historical outages have a greater end time than the current time - error
            //If historical outages end time is greater than the start time - error
            return true;
        }

        public bool ValidatePredicted(Predicted outage)
        {
            //If they are supposed to be numbers, check that
            if (!(isNumeric(outage.Reference)
                && isNumeric(outage.SatelliteVehicleId)
                && isNumeric(outage.SatelliteVehicleNumber)
                && isNumeric(outage.StartYear)
                && isNumeric(outage.StartDayOfYear)
                && isNumeric(outage.StartHour)
                && isNumeric(outage.StartMinute)
                && isNumeric(outage.StartSecond)
                ))
            {
                //TODO: Log error
                return false;
            }
            //If they are NOT supposed to be numbers, check that
            if (outage.Name.Any(char.IsDigit) || outage.Type.Any(char.IsDigit))
            {
                return false;
            }
            return true;
        }

        public bool ValidateCurrent(Current outage)
        {
            //If they are supposed to be numbers, check that
            if (!(isNumeric(outage.Reference)
                && isNumeric(outage.SatelliteVehicleId)
                && isNumeric(outage.SatelliteVehicleNumber)
                && isNumeric(outage.StartYear)
                && isNumeric(outage.StartDayOfYear)
                && isNumeric(outage.StartHour)
                && isNumeric(outage.StartMinute)
                && isNumeric(outage.StartSecond)
                ))
            {
                //TODO: Log error
                return false;
            }
            //If they are NOT supposed to be numbers, check that
            if (outage.Name.Any(char.IsDigit) || outage.Type.Any(char.IsDigit))
            {
                return false;
            }
            //If current outages have a greater start time than current time - error

            //If all tests pass, return true
            return true;
        }

        /// <summary>
        /// Takes in a string and determines whether it is a valid integer (no letters)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool isNumeric(string s)
        {
            return int.TryParse(s, out int n);
        }
    }
}
