﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GPS.Data.ParserObjects
{
	public class Current
	{
		/// <summary>
		/// Reusable identifier for each satellite in identified system
		/// </summary>
		[XmlAttribute("SVID")]
		public string SatelliteVehicleId { get; set; }

		/// <summary>
		/// Unique sequential number associated with satellite-specific program; assigned by the U.S. Air Force
		/// </summary>
		[XmlAttribute("SVN")]
		public string SatelliteVehicleNumber { get; set; }

		/// <summary>
		/// Alphanumeric indicator of outage source
		/// </summary>
		[XmlAttribute("NAME")]
		public string Name { get; set; }

		[XmlAttribute("TYPE")]
		public string Type { get; set; }

		[XmlAttribute("REFERENCE")]
		public string Reference { get; set; }

		/// <summary>
		/// Year when the outage began
		/// </summary>
		[XmlAttribute("START_YEAR")]
		public string StartYear { get; set; }

		/// <summary>
		/// Day of year (1—365) when the outage began
		/// </summary>
		[XmlAttribute("START_DOY")]
		public string StartDayOfYear { get; set; }

		/// <summary>
		/// Hour when the outage began
		/// </summary>
		[XmlAttribute("START_HR")]
		public string StartHour { get; set; }

		/// <summary>
		/// Minute when the outage began
		/// </summary>
		[XmlAttribute("START_MIN")]
		public string StartMinute { get; set; }

		/// <summary>
		/// Second when the outage began
		/// </summary>
		[XmlAttribute("START_SEC")]
		public string StartSecond { get; set; }
	}
}
