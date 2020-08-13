using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsrsPriceChecker.Connector;
using OsrsPriceChecker.Data;

namespace OsrsPriceChecker.Common
{
	public class DataRetriever
	{
		#region Variables
		private WebAPI webAPI;

		private List<CoreItemData> allItemData = new List<CoreItemData>();
		private static List<CoreItemData> filteredData = new List<CoreItemData>();
		#endregion Variables

		public void FetchCoreData()
		{
			Console.WriteLine("\nFetching all core data...");

			webAPI = new WebAPI();

			allItemData = webAPI.FetchCoreItemData();

			// allWeaponData = Program.webAPI.FetchCoreWeaponData();

			// allEquipmentData = Program.webAPI.FetchCoreEquipmentData();

			Console.WriteLine("Done!");
		}

		public void ParseItemData(string userInput)
		{
			Console.WriteLine($"\nSearching for 'item': '{userInput}'...\n");

			for (int i = 0; i < allItemData.Count; i++)
			{
				if (allItemData[i].Name.ToLower().Contains(userInput.ToLower()))
				{
					filteredData.Add(allItemData[i]);
				}
			}

			ParseFilteredItemData();
		}

		private void ParseFilteredItemData()
		{
			List<string> jsonStrings = webAPI.ReturnFilteredItemResults(filteredData);

			List<Item> items = new List<Item>();
			for (int i = 0; i < jsonStrings.Count; i++)
			{
				if (string.IsNullOrEmpty(jsonStrings[i]))
				{
					continue;
				}

				items.Add(JsonConvert.DeserializeObject<Item>(jsonStrings[i]));
			}

			DisplayCostOfItems(items);
		}

		private void DisplayCostOfItems(List<Item> items)
		{
			for (int i = 0; i < items.Count; i++)
			{
				Console.WriteLine($"The cost of '{items[i].Name}' is: {items[i].Cost.ToString("N0")}gp");
			}

			Program.userInputHandler.EndSearchSequence();
		}
	}
}