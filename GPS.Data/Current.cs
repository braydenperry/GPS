using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GPS.Data
{
	public class Current
	{
		/// <summary>
		/// Reusable identifier for each satellite in identified system
		/// </summary>
		[XmlAttribute("SVID")]
		public int SatelliteVehicleId { get; set; }

		/// <summary>
		/// Unique sequential number associated with satellite-specific program; assigned by the U.S. Air Force
		/// </summary>
		[XmlAttribute("SVN")]
		public int SatelliteVehicleNumber { get; set; }

		/// <summary>
		/// Alphanumeric indicator of outage source
		/// </summary>
		[XmlAttribute("NAME")]
		public string Name { get; set; }

		[XmlAttribute("TYPE")]
		public string Type { get; set; }

		[XmlAttribute("REFERENCE")]
		public int Reference { get; set; }

		/// <summary>
		/// Year when the outage began
		/// </summary>
		[XmlAttribute("START_YEAR")]
		public int StartYear { get; set; }

		/// <summary>
		/// Day of year (1—365) when the outage began
		/// </summary>
		[XmlAttribute("START_DOY")]
		public int StartDayOfYear { get; set; }

		/// <summary>
		/// Hour when the outage began
		/// </summary>
		[XmlAttribute("START_HR")]
		public int StartHour { get; set; }

		/// <summary>
		/// Minute when the outage began
		/// </summary>
		[XmlAttribute("START_MIN")]
		public int StartMinute { get; set; }

		/// <summary>
		/// Second when the outage began
		/// </summary>
		[XmlAttribute("START_SEC")]
		public int StartSecond { get; set; }
	}
}
