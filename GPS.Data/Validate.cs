using GPS.Data.ParserObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS.Data
{
	public class Validate
	{
		DateTime CurrentTime = DateTime.UtcNow;
		public bool ValidateHistorical(Historical outage)
		{
			//Validate the following are numbers
			if (!(isNumeric(outage.Reference)
				&& isNumeric(outage.SatelliteVehicleId)
				&& isNumeric(outage.SatelliteVehicleNumber)
				&& isNumeric(outage.StartYear)
				&& isNumeric(outage.StartDayOfYear)
				&& isNumeric(outage.StartHour)
				&& isNumeric(outage.StartMinute)
				&& isNumeric(outage.StartSecond)
				&& isNumeric(outage.EndYear)
				&& isNumeric(outage.EndDayOfYear)
				&& isNumeric(outage.EndHour)
				&& isNumeric(outage.EndMinute)
				&& isNumeric(outage.EndSecond)
				))
			{
				//TODO: Log error
				return false;
			}

			//create start and end time varibles for Historical outage
			DateTime StartTime = GpsIsFile.ToDateTime(int.Parse(outage.StartYear), int.Parse(outage.StartDayOfYear), int.Parse(outage.StartHour), int.Parse(outage.StartMinute), int.Parse(outage.StartSecond));
			DateTime EndTime = GpsIsFile.ToDateTime(int.Parse(outage.EndYear), int.Parse(outage.EndDayOfYear), int.Parse(outage.EndHour), int.Parse(outage.EndMinute), int.Parse(outage.EndSecond));

			//Validate name and type are NOT numbers
			if (outage.Name.Any(char.IsDigit) || outage.Type.Any(char.IsDigit))
			{
				//TODO: Log error
				return false;
			}

			//Validate that historical outage end time is not greater than historical outage start time
			//or greater than current time
			if (EndTime > CurrentTime || EndTime < StartTime)
			{
				return false;
			}

			//If all tests pass, return true
			return true;
		}

		/// <summary>
		/// Returns 1 if valid and end time exists
		/// Returns 2 if valid and NO end time
		/// Returns 3 if invalid
		/// </summary>
		/// <param name="outage"></param>
		/// <returns></returns>
		public int ValidatePredicted(Predicted outage)
		{
			bool endYearExists = false;
			//if end time exists
			if (outage.EndYear != null)
			{
				endYearExists = true;
				//Validate the following are numbers
				if (!(isNumeric(outage.Reference)
					&& isNumeric(outage.SatelliteVehicleId)
					&& isNumeric(outage.SatelliteVehicleNumber)
					&& isNumeric(outage.StartYear)
					&& isNumeric(outage.StartDayOfYear)
					&& isNumeric(outage.StartHour)
					&& isNumeric(outage.StartMinute)
					&& isNumeric(outage.StartSecond)
					&& isNumeric(outage.EndYear)
					&& isNumeric(outage.EndDayOfYear)
					&& isNumeric(outage.EndHour)
					&& isNumeric(outage.EndMinute)
					&& isNumeric(outage.EndSecond)
					))
				{
					//TODO: Log error
					return 3;
				}
				//create start and end time varibles for Predicted outage
				DateTime StartTime = GpsIsFile.ToDateTime(int.Parse(outage.StartYear), int.Parse(outage.StartDayOfYear), int.Parse(outage.StartHour), int.Parse(outage.StartMinute), int.Parse(outage.StartSecond));
				DateTime EndTime = GpsIsFile.ToDateTime(int.Parse(outage.EndYear), int.Parse(outage.EndDayOfYear), int.Parse(outage.EndHour), int.Parse(outage.EndMinute), int.Parse(outage.EndSecond));

				//Validate start time is less than end time
				if (StartTime > EndTime)
				{
					return 3;
				}
			}
			//if end time does not exist
			else
			{
				//Validate the following are numbers
				if (!(isNumeric(outage.Reference)
					&& isNumeric(outage.SatelliteVehicleId)
					&& isNumeric(outage.SatelliteVehicleNumber)
					&& isNumeric(outage.StartYear)
					&& isNumeric(outage.StartDayOfYear)
					&& isNumeric(outage.StartHour)
					&& isNumeric(outage.StartMinute)
					&& isNumeric(outage.StartSecond)
					))
				{
					//TODO: Log error
					return 3;
				}
			}

			//Validate name and type are NOT numbers
			if (outage.Name.Any(char.IsDigit) || outage.Type.Any(char.IsDigit))
			{
				return 3;
			}

			//If all tests pass, return true
			if (endYearExists)
			{ 
				return 1;
			}
			else
			{
				return 2;
			}
		}

		public bool ValidateCurrent(Current outage)
		{
			//Validate the following are numbers
			if (!(isNumeric(outage.Reference)
				&& isNumeric(outage.SatelliteVehicleId)
				&& isNumeric(outage.SatelliteVehicleNumber)
				&& isNumeric(outage.StartYear)
				&& isNumeric(outage.StartDayOfYear)
				&& isNumeric(outage.StartHour)
				&& isNumeric(outage.StartMinute)
				&& isNumeric(outage.StartSecond)
				))
			{
				//TODO: Log error
				return false;
			}

			//create start varible for current outage
			DateTime StartTime = GpsIsFile.ToDateTime(int.Parse(outage.StartYear), int.Parse(outage.StartDayOfYear), int.Parse(outage.StartHour), int.Parse(outage.StartMinute), int.Parse(outage.StartSecond));

			//Validate name and type are NOT numbers
			if (outage.Name.Any(char.IsDigit) || outage.Type.Any(char.IsDigit))
			{
				return false;
			}

			//Validate that current outage StartTime is not greater than CurrentTime
			if (StartTime > CurrentTime)
			{
				return false;
			}

			//If all tests pass, return true
			return true;
		}

		/// <summary>
		/// Takes in a string and determines whether it is a valid integer (no letters)
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private bool isNumeric(string s)
		{
			return int.TryParse(s, out int n);
		}
	}
}
