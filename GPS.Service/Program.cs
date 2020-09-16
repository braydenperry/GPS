using GPS.Data;
using System;
using System.Collections.Generic;

namespace GPS.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get a list of all outages from the .sof file
            Parser sofParser = new Parser("current.sof");
            List<Outage> allOutages = sofParser.PopulateObjectsFromSof();

            //This is just a test to make sure everything was populated
            //this will be moved to the xunit test later
            foreach (Outage outages in allOutages)
            {
                Console.WriteLine(outages.StartYear);
            }

            Console.WriteLine("I'm trying to break stuff... but not really. Please work!");
        }
    }
}
