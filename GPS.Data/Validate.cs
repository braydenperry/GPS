using GPS.Data.ParserObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.Data
{
    public class Validate
    {
        public bool ValidateHistorical(Historical outage)
        {
            //If they are supposed to be numbers, check that ----------------------------------- try catch block should do
            //If they are NOT supposed to be numbers, check that
            //If historical outages have a greater end time than the current time - error
            //If historical outages end time is greater than the start time - error
            return true;
        }

        public bool ValidatePredicted(Predicted outage)
        {
            //If they are supposed to be numbers, check that ---------------------------------- try catch block should do
            //If they are NOT supposed to be numbers, check that
            return true;
        }

        public bool ValidateCurrent(Current outage)
        {
            //If they are supposed to be numbers, check that ---------------------------------- try catch block should do
            //If they are NOT supposed to be numbers, check that
            //If current outages have a greater start time than current time - error
            return true;
        }
    }
}
