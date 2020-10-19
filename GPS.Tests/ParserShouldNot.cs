using GPS.Data;
using GPS.Data.ParserObjects;
using System.Collections.Generic;
using Xunit;

namespace GPS.Test
{
    public class ParserShouldNot
    {
        //create object for parser and allOutages
        private readonly Parser _parser;
        private readonly List<Outage> _allOutages;

        /// <summary>
        /// Load data into parser and allOutages
        /// </summary>
        public ParserShouldNot()
        {
            _parser = new Parser("SOF\\invalidTest.sof");
            _allOutages = _parser.PopulateObjectsFromSof();
        }

        //if all outages is null - pass test
        /// <summary>
        /// Test to make sure the _allOutages list is empty
        /// Every tag in invalidTest.sof is an invalid entry, therefore none should pass the validation checks in Validate.cs
        /// </summary>
        [Fact]
        public void YOUSHALLNOTPASS()
        {
            //_parser = new Parser("SOF\\validTest.sof");
            //_allOutages = _parser.PopulateObjectsFromSof();
            Assert.Empty(_allOutages);
        }
    }
}
