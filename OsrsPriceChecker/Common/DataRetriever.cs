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
		private List<CoreItemData> filteredData = new List<CoreItemData>();
		private List<Item> items = new List<Item>();

		public List<CoreItemData> FilteredData { get => filteredData; }
		#endregion Variables

		public async void ParseItemData(string userInput)
		{
			Console.WriteLine(string.Format("\nSearching for 'item': {0}...", userInput));

			for (int i = 0; i < Program.webAPI.AllItems.Count; i++)
			{
				if (Program.webAPI.AllItems[i].Name.ToLower().Contains(userInput.ToLower()))
				{
					filteredData.Add(Program.webAPI.AllItems[i]);
				}
			}

			await Program.webAPI.ParseFilteredItemData();
		}

		public async void GetItemData(ItemType type, string userInput)
		{
			try
			{
				string jsonString = "";

				switch (type)
				{
					case ItemType.Item:
						break;
					case ItemType.Weapon:
						await Program.webAPI.ParseEndPoint(ItemType.Weapon, userInput);
						break;
					case ItemType.Equipment:
						await Program.webAPI.ParseEndPoint(ItemType.Equipment, userInput);
						break;
				}

				DeserializeJson(jsonString);
			}
			catch (Exception e)
			{
				Console.WriteLine("Operation failed. Exiting...");
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
		}
	}
}