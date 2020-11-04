using GPS.Data;
using GPS.Data.ParserObjects;
using System.Collections.Generic;
using Xunit;

namespace GPS.Test
{
	public class ParserShould
	{
		//create object for parser and allOutages
		private readonly Parser _parser;
		private readonly List<Outage> _allOutages;


		/// <summary>
		/// Load data into parser and allOutages
		/// </summary>
		public ParserShould()
		{
			_parser = new Parser("validTest.sof");
			_allOutages = _parser.PopulateObjectsFromSof();
		}

		#region Tests to check parser loaded correctly
		/// <summary>
		/// The following tests check to make sure the sof file loaded into the parser correctly
		/// </summary>
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
		#endregion

		/// <summary>
		/// Test to check the creation file is correct
		/// </summary>
		[Fact]
		public void CheckCreationTagAttributes()
		{
			Assert.Equal("2020", _parser.Outages.Creation.Year);
			Assert.Equal("226", _parser.Outages.Creation.DayOfYear);
			Assert.Equal("12", _parser.Outages.Creation.Hour);
			Assert.Equal("55", _parser.Outages.Creation.Minute);
			Assert.Equal("4", _parser.Outages.Creation.Second);
		}

		/// <summary>
		/// Test to check the reference is correct
		/// </summary>
		[Fact]
		public void CheckReferenceTagAttributes()
		{
			Assert.Equal("2020", _parser.Outages.Reference.Year);
			Assert.Equal("226", _parser.Outages.Reference.DayOfYear);
			Assert.Equal("12", _parser.Outages.Reference.Hour);
			Assert.Equal("55", _parser.Outages.Reference.Minute);
			Assert.Equal("4", _parser.Outages.Reference.Second);
		}

		/// <summary>
		/// Test to check the first historical tag
		/// </summary>
		[Fact]
		public void CheckFirstHistoricalTag()
		{
			Assert.Equal("HISTORICAL", _allOutages[0].TagName);
			Assert.Equal("8", _allOutages[0].SpaceVehicleId);
			Assert.Equal("38", _allOutages[0].SpaceVehicleNumber);
			Assert.Equal("NANU", _allOutages[0].Name);
			Assert.Equal("UNUSABLE", _allOutages[0].Type);
			Assert.Equal("1998006", _allOutages[0].Reference);
			Assert.True(GpsIsFile.ToDateTime(1998, 009, 09, 55, 00) == _allOutages[0].StartTime);
			Assert.True(GpsIsFile.ToDateTime(1998, 009, 22, 56, 00) == _allOutages[0].EndTime);
		}

		/// <summary>
		/// Test to check the second historical tag
		/// </summary>
		[Fact]
		public void CheckSecondHistoricalTag()
		{
			Assert.Equal("HISTORICAL", _allOutages[1].TagName);
			Assert.Equal("24", _allOutages[1].SpaceVehicleId);
			Assert.Equal("24", _allOutages[1].SpaceVehicleNumber);
			Assert.Equal("NANU", _allOutages[1].Name);
			Assert.Equal("FCSTSUMM", _allOutages[1].Type);
			Assert.Equal("1998014", _allOutages[1].Reference);
			Assert.True(GpsIsFile.ToDateTime(1998, 021, 01, 27, 00) == _allOutages[1].StartTime);
			Assert.True(GpsIsFile.ToDateTime(1998, 021, 03, 42, 00) == _allOutages[1].EndTime);
		}

		/// <summary>
		/// Test to check the first current tag
		/// </summary>
		[Fact]
		public void CheckFirstCurrentTag()
		{
			Assert.Equal("CURRENT", _allOutages[2].TagName);
			Assert.Equal("14", _allOutages[2].SpaceVehicleId);
			Assert.Equal("41", _allOutages[2].SpaceVehicleNumber);
			Assert.Equal("NANU", _allOutages[2].Name);
			Assert.Equal("FCSTUUFN", _allOutages[2].Type);
			Assert.Equal("2020031", _allOutages[2].Reference);
			Assert.True(GpsIsFile.ToDateTime(2020, 191, 18, 00, 00) == _allOutages[2].StartTime);
		}

		/// <summary>
		/// Test to check the second current tag
		/// </summary>
		[Fact]
		public void CheckSecondCurrentTag()
		{
			Assert.Equal("CURRENT", _allOutages[3].TagName);
			Assert.Equal("23", _allOutages[3].SpaceVehicleId);
			Assert.Equal("76", _allOutages[3].SpaceVehicleNumber);
			Assert.Equal("NANU", _allOutages[3].Name);
			Assert.Equal("UNUSUFN", _allOutages[3].Type);
			Assert.Equal("2020033", _allOutages[3].Reference);
			Assert.True(GpsIsFile.ToDateTime(2020, 182, 20, 10, 00) == _allOutages[3].StartTime);
		}

		/// <summary>
		/// Test to check the first predicted tag
		/// </summary>
		[Fact]
		public void CheckFirstPredictedTag()
		{
			Assert.Equal("PREDICTED", _allOutages[4].TagName);
			Assert.Equal("23", _allOutages[4].SpaceVehicleId);
			Assert.Equal("60", _allOutages[4].SpaceVehicleNumber);
			Assert.Equal("NANU", _allOutages[4].Name);
			Assert.Equal("FCSTUUFN", _allOutages[4].Type);
			Assert.Equal("2020010", _allOutages[4].Reference);
			Assert.True(GpsIsFile.ToDateTime(2020, 069, 15, 30, 00) == _allOutages[4].StartTime);
		}

		/// <summary>
		/// Test to check the second predicted tag
		/// </summary>
		[Fact]
		public void CheckSecondPredictedTag()
		{
			Assert.Equal("PREDICTED", _allOutages[5].TagName);
			Assert.Equal("25", _allOutages[5].SpaceVehicleId);
			Assert.Equal("62", _allOutages[5].SpaceVehicleNumber);
			Assert.Equal("NANU", _allOutages[5].Name);
			Assert.Equal("FCSTMX", _allOutages[5].Type);
			Assert.Equal("2020045", _allOutages[5].Reference);
			Assert.True(GpsIsFile.ToDateTime(2020, 275, 19, 00, 00) == _allOutages[5].StartTime);
			Assert.True(GpsIsFile.ToDateTime(2020, 277, 19, 00, 00) == _allOutages[5].EndTime);
		}
	}
}
