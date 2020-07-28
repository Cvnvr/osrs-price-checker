using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OsrsPriceChecker
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

	public class ItemsList
	{
		[JsonProperty("_items")]
		public List<Item> Items { get; set; }
	}

	public class Item
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("members")]
		public bool IsMembers { get; set; }

		[JsonProperty("cost")]
		public int Cost { get; set; }

		[JsonProperty("lowalch")]
		public int? LowAlchPrice { get; set; }

		[JsonProperty("highalch")]
		public int? HighAlchPrice { get; set; }
	}
}