using GPS.Data;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPS.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get a list of all outages from the .sof file
            Parser sofParser = new Parser("SOF\\current.sof");
            List<Outage> allOutages = sofParser.PopulateObjectsFromSof();
            foreach (var outages in

            //Example of LINQ
            from Outage outages in allOutages
            where outages.StartYear == 2020
            select outages)
            {
                Console.WriteLine(outages.StartYear);
            }
        }
    }
}
