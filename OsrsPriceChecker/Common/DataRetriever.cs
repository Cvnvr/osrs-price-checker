using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsrsPriceChecker
{
	class DataRetriever
	{
		#region Variables
		private WebAPI api;

		// Result from GET request
		private List<Item> items = new List<Item>();
		#endregion Variables

		public async void MakeTheRequest(ItemType type, string userInput)
		{
			api = new WebAPI();
			string json = "";

			switch (type)
			{
				case ItemType.Item:
					json = await api.HttpRequest(ItemType.Item, RequestType.GET, userInput);
					break;
				case ItemType.Weapon:
					json = await api.HttpRequest(ItemType.Weapon, RequestType.GET, userInput);
					break;
				case ItemType.Equipment:
					json = await api.HttpRequest(ItemType.Equipment, RequestType.GET, userInput);
					break;
			}

			ParseJson(json);
		}

		private void ParseJson(string jsonString)
		{
			if (string.IsNullOrEmpty(jsonString))
			{
				return;
			}

			Console.WriteLine(jsonString);
			Console.ReadLine();
			// List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonString);
		}
	}
}