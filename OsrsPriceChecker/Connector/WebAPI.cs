using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OsrsPriceChecker.Connector
{
	public class WebAPI
	{
		#region Variables
		// Items
		private readonly string allItemsEndPoint = "https://raw.githubusercontent.com/osrsbox/osrsbox-db/master/docs/items-search.json";
		private readonly string itemEndPoint = "https://api.osrsbox.com/items";

		// Weapons
		private readonly string allWeaponsEndPoint = "";
		private readonly string weaponEndPoint = "https://api.osrsbox.com/weapons";

		// Equipment
		private readonly string allEquipmentEndPoint = "";
		private readonly string equipmentEndPoint = "https://api.osrsbox.com/equipment";
		#endregion Variables

		public List<CoreItemData> FetchCoreItemData()
		{
			string jsonString = HttpRequest(allItemsEndPoint);
			if (string.IsNullOrEmpty(jsonString))
			{
				return null;
			}

			// Parse json string into data object
			Dictionary<string, CoreItemData> myDict = JsonConvert.DeserializeObject<Dictionary<string, CoreItemData>>(jsonString);
			List<CoreItemData> allItems = new List<CoreItemData>(myDict.Values);

			// Remove duplicates from list
			for (int i = allItems.Count - 1; i >= 0; i--)
			{
				if (allItems[i].Duplicate)
				{
					allItems.RemoveAt(i);
				}
			}

			return allItems;
		}

		public List<string> ReturnFilteredItemResults(List<CoreItemData> filteredData)
		{
			List<string> jsonStrings = new List<string>();

			for (int i = 0; i < filteredData.Count; i++)
			{
				string endPoint = $"{itemEndPoint}/{filteredData[i].Id}";
				jsonStrings.Add(HttpRequest(endPoint));
			}

			return jsonStrings;
		}

		private string HttpRequest(string endPoint)
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