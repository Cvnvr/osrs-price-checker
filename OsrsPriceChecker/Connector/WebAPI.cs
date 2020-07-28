using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OsrsPriceChecker
{
	public enum ItemType { Item, Weapon, Equipment };

	public class WebAPI
	{
		#region Variables
		// All items data
		private readonly string allItemsEndPoint = "https://raw.githubusercontent.com/osrsbox/osrsbox-db/master/docs/items-search.json";
		private List<CoreItemData> allItems = new List<CoreItemData>();

		// Query end points
		private readonly string itemEndPoint = "https://api.osrsbox.com/items";
		private readonly string weaponEndPoint = "https://api.osrsbox.com/weapons";
		private readonly string equipmentEndPoint = "https://api.osrsbox.com/equipment";

		public List<CoreItemData> AllItems { get => allItems; }
		#endregion Variables

		public async Task GetCoreItemData()
		{
			string jsonString = await HttpRequest(allItemsEndPoint);

			if (string.IsNullOrEmpty(jsonString))
			{
				return;
			}

			// Parse json string into data object
			Dictionary<string, CoreItemData> myDict = JsonConvert.DeserializeObject<Dictionary<string, CoreItemData>>(jsonString);
			allItems = new List<CoreItemData>(myDict.Values);

			// Remove duplicates from list
			for (int i = allItems.Count - 1; i >= 0; i--)
			{
				if (allItems[i].Duplicate)
				{
					allItems.RemoveAt(i);
				}
			}
		}

		public async Task ParseFilteredItemData()
		{
			List<string> jsonStrings = new List<string>();

			for (int i = 0; i < Program.dataRetriever.FilteredData.Count; i++)
			{
				string endPoint = $"{itemEndPoint}/{Program.dataRetriever.FilteredData[i].Id}";
				jsonStrings.Add(await HttpRequest(endPoint));
			}

			List<Item> items = new List<Item>();
			for (int i = 0; i < jsonStrings.Count; i++)
			{
				if (string.IsNullOrEmpty(jsonStrings[i]))
				{
					continue;
				}

				items.Add(JsonConvert.DeserializeObject<Item>(jsonStrings[i]));
				Console.WriteLine($"The cost of {items[items.Count - 1].Name} is: {items[items.Count - 1].Cost}");
			}
		}

		public async Task ParseEndPoint(ItemType itemType, string userInput)
		{
			string endPoint = GetCorrespondingEndPoint() + FormatSearchParamater(userInput);
			string jsonString = await HttpRequest(endPoint);

			#region Local Functions
			string GetCorrespondingEndPoint()
			{
				switch (itemType)
				{
					case ItemType.Item:
						return itemEndPoint;
					case ItemType.Weapon:
						return weaponEndPoint;
					case ItemType.Equipment:
						return equipmentEndPoint;
					default:
						return "ERROR";
				}
			}
			#endregion Local Functions
		}

		private string FormatSearchParamater(string userInput)
		{
			// Directly return formatted query if it's a single word
			if (!userInput.Contains(' '))
			{
				return $"?where={{\"name\":\"{userInput}\"}}";
			}

			// Split sentence into individual words to inject "%20" between them
			string[] words = userInput.Split(' ');
			string userQuery = "";

			for (int i = 0; i < words.Length; i++)
			{
				userQuery += (i == 0) ? words[i] : string.Format("%20{0}", words[i]);
			}

			return $"?where={{\"name\":\"{userQuery}\"}}";
		}

		private async Task<string> HttpRequest(string endPoint)
		{
			// Contact end point
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(endPoint);
			webRequest.Method = "GET";

			HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

			// Parse response into JSON
			string jsonString;
			using (Stream stream = webResponse.GetResponseStream())
			{
				StreamReader reader = new StreamReader(stream, Encoding.UTF8);
				jsonString = reader.ReadToEnd();
			}

			return jsonString;
		}
	}
}