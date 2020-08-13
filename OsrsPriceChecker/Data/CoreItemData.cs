using Newtonsoft.Json;

namespace OsrsPriceChecker.Data
{
	public class CoreItemData
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("duplicate")]
		public bool Duplicate { get; set; }
	}
}