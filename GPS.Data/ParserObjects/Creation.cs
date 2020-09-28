using System.Xml.Serialization;

namespace GPS.Data.ParserObjects
{
	/// <summary>
	/// Time and date of the creation of the satellite outage file
	/// </summary>
	public class Creation
	{
		/// <summary>
		/// Year when the Satellite Outage File was created
		/// </summary>
		[XmlAttribute("YEAR")]
		public string Year { get; set; }

		/// <summary>
		/// Day of the year (1—365) when the Satellite Outage File (SOF) was created
		/// </summary>
		[XmlAttribute("DOY")]
		public string DayOfYear { get; set; }

		/// <summary>
		/// Hour when the Satellite Outage File was (SOF) created
		/// </summary>
		[XmlAttribute("HR")]
		public string Hour { get; set; }

		/// <summary>
		/// Minute when the Satellite Outage File (SOF) was created
		/// </summary>
		[XmlAttribute("MIN")]
		public string Minute { get; set; }

		/// <summary>
		/// Second when the Satellite Outage File (SOF) was created
		/// </summary>
		[XmlAttribute("SEC")]
		public string Second { get; set; }
	}
}