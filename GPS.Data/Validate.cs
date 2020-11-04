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

		/// <summary>
		/// Validate for the creation attribute of the .sof file.
		/// </summary>
		/// <param name="creation"></param>
		/// <returns></returns>
		public bool ValidateCreation(Creation creation)
        {
			//Verify that they are actually numbers.
			if (!(IsNumeric(creation.Year)
			&& IsNumeric(creation.DayOfYear)
			&& IsNumeric(creation.Hour)
			&& IsNumeric(creation.Minute)
			&& IsNumeric(creation.Second)
			))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Validate for the reference attribute of the .sof file.
		/// </summary>
		/// <param name="creation"></param>
		/// <returns></returns>
		public bool ValidateReference(Reference reference)
		{
			//Verify that they are actually numbers.
			if (!(IsNumeric(reference.Year)
			&& IsNumeric(reference.DayOfYear)
			&& IsNumeric(reference.Hour)
			&& IsNumeric(reference.Minute)
			&& IsNumeric(reference.Second)
			))
			{
				return false;
			}

			return true;
		}

		public bool ValidateHistorical(Historical outage)
		{
			//Validate the following are numbers
			if (!(IsNumeric(outage.Reference)
				&& IsNumeric(outage.SatelliteVehicleId)
				&& IsNumeric(outage.SatelliteVehicleNumber)
				&& IsNumeric(outage.StartYear)
				&& IsNumeric(outage.StartDayOfYear)
				&& IsNumeric(outage.StartHour)
				&& IsNumeric(outage.StartMinute)
				&& IsNumeric(outage.StartSecond)
				&& IsNumeric(outage.EndYear)
				&& IsNumeric(outage.EndDayOfYear)
				&& IsNumeric(outage.EndHour)
				&& IsNumeric(outage.EndMinute)
				&& IsNumeric(outage.EndSecond)
				))
			{
				return false;
			}

			//create start and end time varibles for Historical outage
			DateTime StartTime = GpsIsFile.ToDateTime(int.Parse(outage.StartYear), int.Parse(outage.StartDayOfYear), int.Parse(outage.StartHour), int.Parse(outage.StartMinute), int.Parse(outage.StartSecond));
			DateTime EndTime = GpsIsFile.ToDateTime(int.Parse(outage.EndYear), int.Parse(outage.EndDayOfYear), int.Parse(outage.EndHour), int.Parse(outage.EndMinute), int.Parse(outage.EndSecond));

			//Validate name and type are NOT numbers
			if (outage.Name.Any(char.IsDigit) || outage.Type.Any(char.IsDigit))
			{
				return false;
			}

			//Validate that historical outage end time is not less than start time
			//or greater than current time
			if (EndTime > CurrentTime || EndTime < StartTime)
			{
				return false;
			}

			//If all tests pass, return true
			return true;
		}

		/// <summary>
		/// Enum for the return values of ValidatePredicted. Couldn't put this in the function for some reason.
		/// </summary>
        private enum retVal
        {
			validWithEndTime =1,
			validNoEndTime = 2,
			invalid = 3
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
				if (!(IsNumeric(outage.Reference)
					&& IsNumeric(outage.SatelliteVehicleId)
					&& IsNumeric(outage.SatelliteVehicleNumber)
					&& IsNumeric(outage.StartYear)
					&& IsNumeric(outage.StartDayOfYear)
					&& IsNumeric(outage.StartHour)
					&& IsNumeric(outage.StartMinute)
					&& IsNumeric(outage.StartSecond)
					&& IsNumeric(outage.EndYear)
					&& IsNumeric(outage.EndDayOfYear)
					&& IsNumeric(outage.EndHour)
					&& IsNumeric(outage.EndMinute)
					&& IsNumeric(outage.EndSecond)
					))
				{
					return (int)retVal.invalid;
				}
				//create start and end time varibles for Predicted outage
				DateTime StartTime = GpsIsFile.ToDateTime(int.Parse(outage.StartYear), int.Parse(outage.StartDayOfYear), int.Parse(outage.StartHour), int.Parse(outage.StartMinute), int.Parse(outage.StartSecond));
				DateTime EndTime = GpsIsFile.ToDateTime(int.Parse(outage.EndYear), int.Parse(outage.EndDayOfYear), int.Parse(outage.EndHour), int.Parse(outage.EndMinute), int.Parse(outage.EndSecond));

				//Validate start time is less than end time
				if (StartTime > EndTime)
				{
					return (int)retVal.invalid;
				}
			}
			//if end time does not exist
			else
			{
				//Validate the following are numbers
				if (!(IsNumeric(outage.Reference)
					&& IsNumeric(outage.SatelliteVehicleId)
					&& IsNumeric(outage.SatelliteVehicleNumber)
					&& IsNumeric(outage.StartYear)
					&& IsNumeric(outage.StartDayOfYear)
					&& IsNumeric(outage.StartHour)
					&& IsNumeric(outage.StartMinute)
					&& IsNumeric(outage.StartSecond)
					))
				{
					return (int)retVal.invalid;
				}
			}

			//Validate name and type are NOT numbers
			if (outage.Name.Any(char.IsDigit) || outage.Type.Any(char.IsDigit))
			{
				return (int)retVal.invalid;
			}

			//If all tests pass, return true
			if (endYearExists)
			{
				return (int)retVal.validWithEndTime;
			}
			else
			{
				return (int)retVal.validNoEndTime;
			}
		}

		public bool ValidateCurrent(Current outage)
		{
			//Validate the following are numbers
			if (!(IsNumeric(outage.Reference)
				&& IsNumeric(outage.SatelliteVehicleId)
				&& IsNumeric(outage.SatelliteVehicleNumber)
				&& IsNumeric(outage.StartYear)
				&& IsNumeric(outage.StartDayOfYear)
				&& IsNumeric(outage.StartHour)
				&& IsNumeric(outage.StartMinute)
				&& IsNumeric(outage.StartSecond)
				))
			{
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
		private bool IsNumeric(string s)
		{
			return int.TryParse(s, out int n);
		}
	}
}
