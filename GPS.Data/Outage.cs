using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using GPS.Data.ParserObjects;

namespace GPS.Data
{
	public class Outage
	{
		public string TagName { get; set; }

		[XmlAttribute("SVID")]
		public string SatelliteVehicleId { get; set; }

		[XmlAttribute("SVN")]
		public string SatelliteVehicleNumber { get; set; }

		[XmlAttribute("NAME")]
		public string Name { get; set; }

		[XmlAttribute("TYPE")]
		public string Type { get; set; }

		[XmlAttribute("REFERENCE")]
		public string Reference { get; set; }

		[XmlAttribute("START_YEAR")]
		public string StartYear { get; set; }

		[XmlAttribute("START_DOY")]
		public string StartDayOfYear { get; set; }

		[XmlAttribute("START_HR")]
		public string StartHour { get; set; }

		[XmlAttribute("START_MIN")]
		public string StartMinute { get; set; }

		[XmlAttribute("START_SEC")]
		public string StartSecond { get; set; }

		[XmlAttribute("END_YEAR")]
		public string EndYear { get; set; }

		[XmlAttribute("END_DOY")]
		public string EndDayOfYear { get; set; }

		[XmlAttribute("END_HR")]
		public string EndHour { get; set; }

		[XmlAttribute("END_MIN")]
		public string EndMinute { get; set; }

		[XmlAttribute("END_SEC")]
		public string EndSecond { get; set; }
	}
}
