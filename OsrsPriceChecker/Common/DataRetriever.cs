using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OsrsPriceChecker
{
	public class DataRetriever
	{
		#region Variables
		private WebAPI api;

		private List<Item> items = new List<Item>();
		#endregion Variables

		public async void GetItemData(ItemType type, string userInput)
		{
			try
			{
				api = new WebAPI();
				string jsonString = "";

				switch (type)
				{
					case ItemType.Item:
						jsonString = await api.HttpRequest(ItemType.Item, userInput);
						break;
					case ItemType.Weapon:
						jsonString = await api.HttpRequest(ItemType.Weapon, userInput);
						break;
					case ItemType.Equipment:
						jsonString = await api.HttpRequest(ItemType.Equipment, userInput);
						break;
				}

				DeserializeJson(jsonString);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private void DeserializeJson(string jsonString)
		{
			if (string.IsNullOrEmpty(jsonString))
			{
				return;
			}

			ItemsList itemsList = JsonConvert.DeserializeObject<ItemsList>(jsonString);
			items = itemsList.Items;

			for (int i = 0; i < items.Count; i++)
			{
				Console.WriteLine(string.Format("Name: {0}, \t\tCost: {1}", items[i].Name, items[i].Cost.ToString("N0")));
			}
		}
	}
}