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

		public string SatelliteVehicleId { get; set; }

		public string SatelliteVehicleNumber { get; set; }

		public string Name { get; set; }

		public string Type { get; set; }

		public string Reference { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime? EndTime { get; set; }
	}
}
