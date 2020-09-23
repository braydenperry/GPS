using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GPS.Data
{
	public class Outage
	{
		public string TagName { get; set; }

		[XmlAttribute("SVID")]
		public string SatelliteVehicleId { get; set; }

		[XmlAttribute("SVN")]
		public string SatelliteVehicleNumber { get; set; }

		public string Name { get; set; }

		public string Type { get; set; }

		[XmlAttribute("REFERENCE")]
		public string Reference { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }
	}
}
