using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GPS.Data.ParserObjects
{
	/// <summary>
	/// Time and date up to which the satellite outage data was collected
	/// </summary>
	public class Reference
	{
		/// <summary>
		/// Year up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("YEAR")]
		public int Year { get; set; }

		/// <summary>
		/// Day of the year (1—365) up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("DOY")]
		public int DayOfYear { get; set; }

		/// <summary>
		/// Hour up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("HR")]
		public int Hour { get; set; }

		/// <summary>
		/// Minute up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("MIN")]
		public int Minute { get; set; }

		/// <summary>
		/// Second up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("SEC")]
		public int Second { get; set; }
	}
}
