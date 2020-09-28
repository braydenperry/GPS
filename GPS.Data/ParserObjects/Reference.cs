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
		public string Year { get; set; }

		/// <summary>
		/// Day of the year (1—365) up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("DOY")]
		public string DayOfYear { get; set; }

		/// <summary>
		/// Hour up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("HR")]
		public string Hour { get; set; }

		/// <summary>
		/// Minute up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("MIN")]
		public string Minute { get; set; }

		/// <summary>
		/// Second up to which the satellite outage data collected
		/// </summary>
		[XmlAttribute("SEC")]
		public string Second { get; set; }
	}
}
