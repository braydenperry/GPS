using GPS.Data.ParserObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace GPS.Data
{
	public class Parser
	{
		private readonly XmlSerializer Serializer;

		public GpsIsFile Outages { get; set; }

		private readonly List<string> _errorLog = new List<string>();
		/// <summary>
		/// A list of strings containing all the errors that arise from the validation of the .sof file.
		/// </summary>
		public List<string> ErrorLog { get { return _errorLog; } }

		/// <summary>
		/// object in reference to Validate.cs
		/// </summary>
		Validate validateObj = new Validate();

		public Parser(string filePath)
		{
			try
			{
				//Serializes the .sof and populates each of the classes
				Serializer = new XmlSerializer(typeof(GpsIsFile));
				using Stream reader = new FileStream(filePath, FileMode.Open);
				Outages = (GpsIsFile)Serializer.Deserialize(reader);
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("FileNotFoundException");
				//TODO: navigate to an error page if this is the case
				System.Environment.Exit(1);
			}
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
				bool valid = validateObj.ValidateHistorical(historicalOutage);
				if (!valid)
                {
					//If there is an error, log it and continue with the next iteration of the loop
					_errorLog.Add("The Historical tag with the reference number " + historicalOutage.Reference + " is invalid and was not added to the all outages list");
					continue;
				}
					
				//TODO: This is where the error log message will go once we figure out what that's all about.
				

				allOutages.Add(new Outage
				{
					TagName = "HISTORICAL",
					SatelliteVehicleId = historicalOutage.SatelliteVehicleId,
					SatelliteVehicleNumber = historicalOutage.SatelliteVehicleNumber,
					Name = historicalOutage.Name,
					Type = historicalOutage.Type,
					Reference = historicalOutage.Reference,
					StartTime = GpsIsFile.ToDateTime(int.Parse(historicalOutage.StartYear), int.Parse(historicalOutage.StartDayOfYear), int.Parse(historicalOutage.StartHour), int.Parse(historicalOutage.StartMinute), int.Parse(historicalOutage.StartSecond)),
					EndTime = GpsIsFile.ToDateTime(int.Parse(historicalOutage.EndYear), int.Parse(historicalOutage.EndDayOfYear), int.Parse(historicalOutage.EndHour), int.Parse(historicalOutage.EndMinute), int.Parse(historicalOutage.EndSecond))
				});
			}

			foreach (Current currentOutage in Outages.CurrentOutages)
			{
				bool valid = validateObj.ValidateCurrent(currentOutage);
				if (!valid)
				{
					//If there is an error, log it and continue with the next iteration of the loop
					_errorLog.Add("The Curret tag with the reference number " + currentOutage.Reference + " is invalid and was not added to the all outages list");
					continue;
				}

				allOutages.Add(new Outage
				{
					TagName = "CURRENT",
					SatelliteVehicleId = currentOutage.SatelliteVehicleId,
					SatelliteVehicleNumber = currentOutage.SatelliteVehicleNumber,
					Name = currentOutage.Name,
					Type = currentOutage.Type,
					Reference = currentOutage.Reference,
					StartTime = GpsIsFile.ToDateTime(int.Parse(currentOutage.StartYear), int.Parse(currentOutage.StartDayOfYear), int.Parse(currentOutage.StartHour), int.Parse(currentOutage.StartMinute), int.Parse(currentOutage.StartSecond))
				});
			}

			foreach (Predicted predictedOutage in Outages.PredictedOutages)
			{
				int valid = validateObj.ValidatePredicted(predictedOutage);
				//if invalid
				if (valid == 3)
				{
					//If there is an error, log it and continue with the next iteration of the loop
					_errorLog.Add("The Predicted tag with the reference number " + predictedOutage.Reference + " is invalid and was not added to the all outages list");
					continue;
				}

				//if end time exists
				if (valid == 1)
				{
					allOutages.Add(new Outage
					{
						TagName = "PREDICTED",
						SatelliteVehicleId = predictedOutage.SatelliteVehicleId,
						SatelliteVehicleNumber = predictedOutage.SatelliteVehicleNumber,
						Name = predictedOutage.Name,
						Type = predictedOutage.Type,
						Reference = predictedOutage.Reference,
						StartTime = GpsIsFile.ToDateTime(int.Parse(predictedOutage.StartYear), int.Parse(predictedOutage.StartDayOfYear), int.Parse(predictedOutage.StartHour), int.Parse(predictedOutage.StartMinute), int.Parse(predictedOutage.StartSecond)),
						EndTime = GpsIsFile.ToDateTime(int.Parse(predictedOutage.EndYear), int.Parse(predictedOutage.EndDayOfYear), int.Parse(predictedOutage.EndHour), int.Parse(predictedOutage.EndMinute), int.Parse(predictedOutage.EndSecond))
					});
				}
				//if end time does NOT exist
				if (valid == 2)
				{
					allOutages.Add(new Outage
					{
						TagName = "PREDICTED",
						SatelliteVehicleId = predictedOutage.SatelliteVehicleId,
						SatelliteVehicleNumber = predictedOutage.SatelliteVehicleNumber,
						Name = predictedOutage.Name,
						Type = predictedOutage.Type,
						Reference = predictedOutage.Reference,
						StartTime = GpsIsFile.ToDateTime(int.Parse(predictedOutage.StartYear), int.Parse(predictedOutage.StartDayOfYear), int.Parse(predictedOutage.StartHour), int.Parse(predictedOutage.StartMinute), int.Parse(predictedOutage.StartSecond)),
					});
				}
			}

			if (_errorLog == null)
			{
				_errorLog.Add("There were no errors with the current.sof file");
			}
			return allOutages;
		}
	}
}
