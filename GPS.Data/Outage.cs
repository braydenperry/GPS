using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GPS.Data
{
	public class Outage
	{
		public string TagName { get; set; }

		public int SatelliteVehicleId { get; set; }

		public int SatelliteVehicleNumber { get; set; }

		public string Name { get; set; }

		public string Type { get; set; }

		public int Reference { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }
	}
}
