using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OsrsPriceChecker
{
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
        public bool Members { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("lowalch")]
        public int? Lowalch { get; set; }

        [JsonProperty("highalch")]
        public int? Highalch { get; set; }
    }
}