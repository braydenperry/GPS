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
            _parser = new Parser("SOF\\validTest.sof");
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

        [Fact]
        public void CheckCreationTagAttributes()
        {
            Assert.Equal(2020, _parser.Outages.Creation.Year);
            Assert.Equal(226, _parser.Outages.Creation.DayOfYear);
            Assert.Equal(12, _parser.Outages.Creation.Hour);
            Assert.Equal(55, _parser.Outages.Creation.Minute);
            Assert.Equal(4, _parser.Outages.Creation.Second);
        }

        [Fact]
        public void CheckReferenceTagAttributes()
        {
            Assert.Equal(2020, _parser.Outages.Reference.Year);
            Assert.Equal(226, _parser.Outages.Reference.DayOfYear);
            Assert.Equal(12, _parser.Outages.Reference.Hour);
            Assert.Equal(55, _parser.Outages.Reference.Minute);
            Assert.Equal(4, _parser.Outages.Reference.Second);
        }

        [Fact]
        public void CheckFirstHistoricalTag()
        {
            Assert.Equal("HISTORICAL", _allOutages[0].TagName);
            Assert.Equal(8, _allOutages[0].SatelliteVehicleId);
            Assert.Equal(38, _allOutages[0].SatelliteVehicleNumber);
            Assert.Equal("NANU", _allOutages[0].Name);
            Assert.Equal("UNUSABLE", _allOutages[0].Type);
            Assert.Equal(1998006, _allOutages[0].Reference);
            // Assert.Equal(1998, _allOutages[0].StartYear);
            // Assert.Equal(009, _allOutages[0].StartDayOfYear);
            // Assert.Equal(09, _allOutages[0].StartHour);
            // Assert.Equal(55, _allOutages[0].StartMinute);
            // Assert.Equal(00, _allOutages[0].StartSecond);
            // Assert.Equal(1998, _allOutages[0].EndYear);
            // Assert.Equal(009, _allOutages[0].EndDayOfYear);
            // Assert.Equal(22, _allOutages[0].EndHour);
            // Assert.Equal(56, _allOutages[0].EndMinute);
            // Assert.Equal(00, _allOutages[0].EndSecond);
        }

        [Fact]
        public void CheckSecondHistoricalTag()
        {
            Assert.Equal("HISTORICAL", _allOutages[1].TagName);
            Assert.Equal(24, _allOutages[1].SatelliteVehicleId);
            Assert.Equal(24, _allOutages[1].SatelliteVehicleNumber);
            Assert.Equal("NANU", _allOutages[1].Name);
            Assert.Equal("FCSTSUMM", _allOutages[1].Type);
            Assert.Equal(1998014, _allOutages[1].Reference);
            // Assert.Equal(1998, _allOutages[1].StartYear);
            // Assert.Equal(021, _allOutages[1].StartDayOfYear);
            // Assert.Equal(01, _allOutages[1].StartHour);
            // Assert.Equal(27, _allOutages[1].StartMinute);
            // Assert.Equal(00, _allOutages[1].StartSecond);
            // Assert.Equal(1998, _allOutages[1].EndYear);
            // Assert.Equal(021, _allOutages[1].EndDayOfYear);
            // Assert.Equal(03, _allOutages[1].EndHour);
            // Assert.Equal(42, _allOutages[1].EndMinute);
            // Assert.Equal(00, _allOutages[1].EndSecond);
        }

        [Fact]
        public void CheckFirstCurrentTag()
        {
            Assert.Equal("CURRENT", _allOutages[2].TagName);
            Assert.Equal(14, _allOutages[2].SatelliteVehicleId);
            Assert.Equal(41, _allOutages[2].SatelliteVehicleNumber);
            Assert.Equal("NANU", _allOutages[2].Name);
            Assert.Equal("FCSTUUFN", _allOutages[2].Type);
            Assert.Equal(2020031, _allOutages[2].Reference);
            // Assert.Equal(2020, _allOutages[2].StartYear);
            // Assert.Equal(191, _allOutages[2].StartDayOfYear);
            // Assert.Equal(18, _allOutages[2].StartHour);
            // Assert.Equal(00, _allOutages[2].StartMinute);
            // Assert.Equal(00, _allOutages[2].StartSecond);
        }

        [Fact]
        public void CheckSecondCurrentTag()
        {
            Assert.Equal("CURRENT", _allOutages[3].TagName);
            Assert.Equal(23, _allOutages[3].SatelliteVehicleId);
            Assert.Equal(76, _allOutages[3].SatelliteVehicleNumber);
            Assert.Equal("NANU", _allOutages[3].Name);
            Assert.Equal("UNUSUFN", _allOutages[3].Type);
            Assert.Equal(2020033, _allOutages[3].Reference);
            // Assert.Equal(2020, _allOutages[3].StartYear);
            // Assert.Equal(182, _allOutages[3].StartDayOfYear);
            // Assert.Equal(20, _allOutages[3].StartHour);
            // Assert.Equal(10, _allOutages[3].StartMinute);
            // Assert.Equal(00, _allOutages[3].StartSecond);
        }

        [Fact]
        public void CheckFirstPredictedTag()
        {
            Assert.Equal("PREDICTED", _allOutages[4].TagName);
            Assert.Equal(23, _allOutages[4].SatelliteVehicleId);
            Assert.Equal(60, _allOutages[4].SatelliteVehicleNumber);
            Assert.Equal("NANU", _allOutages[4].Name);
            Assert.Equal("FCSTUUFN", _allOutages[4].Type);
            Assert.Equal(2020010, _allOutages[4].Reference);
            // Assert.Equal(2020, _allOutages[4].StartYear);
            // Assert.Equal(069, _allOutages[4].StartDayOfYear);
            // Assert.Equal(15, _allOutages[4].StartHour);
            // Assert.Equal(30, _allOutages[4].StartMinute);
            // Assert.Equal(00, _allOutages[4].StartSecond);
        }

        [Fact]
        public void CheckSecondPredictedTag()
        {
            Assert.Equal("PREDICTED", _allOutages[5].TagName);
            Assert.Equal(18, _allOutages[5].SatelliteVehicleId);
            Assert.Equal(34, _allOutages[5].SatelliteVehicleNumber);
            Assert.Equal("NANU", _allOutages[5].Name);
            Assert.Equal("FCSTUUFN", _allOutages[5].Type);
            Assert.Equal(2019156, _allOutages[5].Reference);
            // Assert.Equal(2019, _allOutages[5].StartYear);
            // Assert.Equal(280, _allOutages[5].StartDayOfYear);
            // Assert.Equal(20, _allOutages[5].StartHour);
            // Assert.Equal(00, _allOutages[5].StartMinute);
            // Assert.Equal(00, _allOutages[5].StartSecond);
        }
    }
}
