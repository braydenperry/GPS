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
		public int SatelliteVehicleId { get; set; }

		[XmlAttribute("SVN")]
		public int SatelliteVehicleNumber { get; set; }

		[XmlAttribute("NAME")]
		public string Name { get; set; }

		[XmlAttribute("TYPE")]
		public string Type { get; set; }

		[XmlAttribute("REFERENCE")]
		public int Reference { get; set; }

		[XmlAttribute("START_YEAR")]
		public int StartYear { get; set; }

		[XmlAttribute("START_DOY")]
		public int StartDayOfYear { get; set; }

		[XmlAttribute("START_HR")]
		public int StartHour { get; set; }

		[XmlAttribute("START_MIN")]
		public int StartMinute { get; set; }

		[XmlAttribute("START_SEC")]
		public int StartSecond { get; set; }

		[XmlAttribute("END_YEAR")]
		public int EndYear { get; set; }

		[XmlAttribute("END_DOY")]
		public int EndDayOfYear { get; set; }

		[XmlAttribute("END_HR")]
		public int EndHour { get; set; }

		[XmlAttribute("END_MIN")]
		public int EndMinute { get; set; }

		[XmlAttribute("END_SEC")]
		public int EndSecond { get; set; }
	}
}
