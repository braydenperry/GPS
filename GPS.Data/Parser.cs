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
        readonly Validate ValidateObj = new Validate();

		/// <summary>
		/// Constructor that is hard-coded to route to the current.sof file.
		/// </summary>
		public Parser()
		{
			try
			{
				// Gets the path of the solution
				string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
				// Combines the path of the working directory with the relative path to the SOF file
				string sofPath = Path.Combine(solutionDirectory, "GPS.Data\\SOF\\current.sof");

				// Deserializes the .sof file and populates each of the classes
				Serializer = new XmlSerializer(typeof(GpsIsFile));
				using Stream reader = new FileStream(sofPath, FileMode.Open);
				Outages = (GpsIsFile)Serializer.Deserialize(reader);
			}
			catch (Exception ex)
			{
				_errorLog.Add(ex.ToString());
			}
		}

		/// <summary>
		/// Overloaded constructor that will accept a file path for testing's sake.
		/// </summary>
		/// <param name="filePath"></param>
		public Parser(string filePath)
		{
			try
			{
				if (filePath == "validTest.sof")
				{
					// Gets the path of the solution
					string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
					// Combines the path of the working directory with the relative path to the SOF file
					string sofPath = Path.Combine(solutionDirectory, "GPS.Data\\SOF\\validTest.sof");

					// Deserializes the .sof file and populates each of the classes
					Serializer = new XmlSerializer(typeof(GpsIsFile));
					using Stream reader = new FileStream(sofPath, FileMode.Open);
					Outages = (GpsIsFile)Serializer.Deserialize(reader);
				}
				else if (filePath == "invalidTest.sof")
				{
					// Gets the path of the solution
					string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
					// Combines the path of the working directory with the relative path to the SOF file
					string sofPath = Path.Combine(solutionDirectory, "GPS.Data\\SOF\\invalidTest.sof");

					// Deserializes the .sof file and populates each of the classes
					Serializer = new XmlSerializer(typeof(GpsIsFile));
					using Stream reader = new FileStream(sofPath, FileMode.Open);
					Outages = (GpsIsFile)Serializer.Deserialize(reader);
				}
			}
			catch (Exception ex)
			{
				_errorLog.Add(ex.ToString());
			}

		}

		/// <summary>
		/// Enum for the return values of ValidatePredicted. Couldn't put this in the function for some reason.
		/// </summary>
		private enum retVal
		{
			validWithEndTime = 1,
			validNoEndTime = 2,
			invalid = 3
		}
		/// <summary>
		/// Takes a .sof and populates the classes with the information included therein
		/// </summary>
		/// <returns>
		/// A list of all outages consolidated in one place for ease of use when querying
		/// </returns>
		public List<Outage> PopulateObjectsFromSof()
		{
			List<Outage> allOutages = new List<Outage>
			{
				new Outage
				{
					TagName = "Creation",
					StartTime = GpsIsFile.ToDateTime(int.Parse(Outages.Creation.Year), int.Parse(Outages.Creation.DayOfYear), int.Parse(Outages.Creation.Hour), int.Parse(Outages.Creation.Minute), int.Parse(Outages.Creation.Second))
				},

				new Outage
				{
					TagName = "Reference",
					StartTime = GpsIsFile.ToDateTime(int.Parse(Outages.Reference.Year), int.Parse(Outages.Reference.DayOfYear), int.Parse(Outages.Reference.Hour), int.Parse(Outages.Reference.Minute), int.Parse(Outages.Reference.Second))
				}
			};

			//Populate the AllOutages List
			List<Outage> allOutages = new List<Outage>();
			foreach (Historical historicalOutage in Outages.HistoricalOutages)
			{
				bool valid = ValidateObj.ValidateHistorical(historicalOutage);
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
					bool valid = ValidateObj.ValidateCurrent(currentOutage);
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
					int valid = ValidateObj.ValidatePredicted(predictedOutage);
					//if invalid
					if (valid == (int)retVal.invalid)
					{
						//If there is an error, log it and continue with the next iteration of the loop
						_errorLog.Add("The Predicted tag with the reference number " + predictedOutage.Reference + " is invalid and was not added to the all outages list");
						continue;
					}

					//if end time exists
					if (valid == (int)retVal.validWithEndTime)
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
					if (valid == (int)retVal.validNoEndTime)
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

				return allOutages;
			}
			catch (Exception ex)
			{
				_errorLog.Add(ex.ToString());
				List<Outage> allOutages = new List<Outage>();
				return allOutages;
			}
		}
	}
}
