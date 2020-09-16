using GPS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GPS.Test
{
    public class ParserShould
    {

        private readonly Parser _parser;
        private readonly List<Outage> _allOutages;

        public ParserShould()
        {
            _parser = new Parser("current.sof");
            _allOutages = _parser.PopulateObjectsFromSof();
        }

        [Fact]
        public void OpenAndParseFile()
        {
            Assert.NotNull(_parser.Outages);
        }

        [Fact]
        public void HaveACreationProperty()
        {
            Assert.NotNull(_parser.Outages.Creation);
        }

        [Fact]
        public void HaveAReferenceProperty()
        {
            Assert.NotNull(_parser.Outages.Reference);
        }

        [Fact]
        public void HaveHistoricalOutages()
        {
            Assert.NotEmpty(_parser.Outages.HistoricalOutages);
        }

        [Fact]
        public void HaveCurrentOutages()
        {
            Assert.NotEmpty(_parser.Outages.CurrentOutages);
        }

        [Fact]
        public void HavePredictedOutages()
        {
            Assert.NotEmpty(_parser.Outages.PredictedOutages);
        }

        [Fact]
        public void PopulateListOfAllOutages()
        {
            Assert.NotEmpty(_allOutages);
        }
    }
}
