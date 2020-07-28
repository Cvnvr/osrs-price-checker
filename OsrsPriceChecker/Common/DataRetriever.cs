﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OsrsPriceChecker.Connector;

namespace OsrsPriceChecker.Common
{
	public class DataRetriever
	{
		#region Variables
		private List<CoreItemData> allItemData = new List<CoreItemData>();
		private static List<CoreItemData> filteredData = new List<CoreItemData>();
		#endregion Variables

		public void FetchCoreData()
		{
			FetchAllItems();
		}

		private void FetchAllItems()
		{
			Console.WriteLine("\nFetching all item data...");

			allItemData = Program.webAPI.FetchCoreItemData();

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
			List<string> jsonStrings = Program.webAPI.ReturnFilteredItemResults(filteredData);

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

			Program.searchHandler.EndSearchSequence();
		}
	}
}