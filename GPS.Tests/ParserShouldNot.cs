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
            _parser = new Parser("invalidTest.sof");
            _allOutages = _parser.PopulateObjectsFromSof();
        }
        /// <summary>
        /// We aren't doing any validation for the creation and reference attributes. 
        /// This function is to check that invalidTest actually exists and populated
        /// the creation and reference attributes correctly. If the file did not exist
        /// then "YOUSHALLNOTPASS()" would pass when it really shouldn't be.
        /// </summary>
        [Fact]
        public void CheckFileExists()
        {
            Assert.Equal("NAN", _parser.Outages.Creation.Year);
            Assert.Equal("226", _parser.Outages.Creation.DayOfYear);
            Assert.Equal("12", _parser.Outages.Creation.Hour);
            Assert.Equal("55", _parser.Outages.Creation.Minute);
            Assert.Equal("4", _parser.Outages.Creation.Second);

        }

        //if all outages is null - pass test
        /// <summary>
        /// Test to make sure the _allOutages list is empty
        /// Every tag in invalidTest.sof is an invalid entry, therefore none should pass the validation checks in Validate.cs
        /// (This test should pass, even though it's named YOUSHALLNOTPASS.)
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
