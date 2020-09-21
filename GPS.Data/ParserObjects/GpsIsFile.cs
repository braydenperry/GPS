using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GPS.Data.ParserObjects
{
	/// <summary>
	/// Global Positioning System Information Service File
	/// </summary>
	[XmlRoot("GPSISFILE")]
	public class GpsIsFile
	{
		#region Properties
		[XmlElement("CREATION")]
		public Creation Creation { get; set; }

		[XmlElement("REFERENCE")]
		public Reference Reference { get; set; }

		/// <summary>
		/// List of satellite outages that took place before the reference time
		/// </summary>
		[XmlElement("HISTORICAL")]
		public List<Historical> HistoricalOutages { get; set; }

		/// <summary>
		/// List of active satellite outages as of the reference time
		/// </summary>
		[XmlElement("CURRENT")]
		public List<Current> CurrentOutages { get; set; }

		/// <summary>
		/// List of satellite outages that are predicted to happen after the reference time
		/// </summary>
		[XmlElement("PREDICTED")]
		public List<Predicted> PredictedOutages { get; set; }
		#endregion

		#region Methods
		/// <summary>
		/// Accepts year, day of year (DOY), hour, minute, and second properties and rolls them into a DateTime object
		/// </summary>
		/// <returns></returns>
		public static DateTime ToDateTime(int year, int DOY, int hour, int minute, int second)
		{
			/* 
             * SOF file uses DOY instead of month and day, so we pass in '1' for month and day.
             * To get the correct month and day, we invoke the AddDays() method and pass in DOY - 1.
             * We -1 because we originally passed in Jan. 1st, not Jan. 0th.
             */
			DateTime date = new DateTime(year, 1, 1, hour, minute, second).AddDays(DOY - 1);
			return date;
		}
		#endregion
	}
}