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
		private List<CoreItemData> allItemData = new List<CoreItemData>();
		private static List<CoreItemData> filteredData = new List<CoreItemData>();
		#endregion Variables

		public void FetchCoreData()
		{
			Console.WriteLine("\nFetching all core data...");

			FetchCoreItemData();

			Console.WriteLine("Done!");
		}

		private void FetchCoreItemData()
		{
			string jsonString = WebAPI.MakeHttpRequest(EndPoints.AllItems);
			if (string.IsNullOrEmpty(jsonString))
			{
				allItemData = new List<CoreItemData>();
				return;
			}

			// Parse json string into data object
			Dictionary<string, CoreItemData> myDict = JsonConvert.DeserializeObject<Dictionary<string, CoreItemData>>(jsonString);
			allItemData = new List<CoreItemData>(myDict.Values);

			// Remove duplicate elements
			allItemData.RemoveAll(item => item.Duplicate);
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
			List<string> jsonStrings = ReturnFilteredItemResults();

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

		private List<string> ReturnFilteredItemResults()
		{
			List<string> jsonStrings = new List<string>();

			for (int i = 0; i < filteredData.Count; i++)
			{
				string endPoint = $"{EndPoints.ItemSearch}/{filteredData[i].Id}";
				jsonStrings.Add(WebAPI.MakeHttpRequest(endPoint));
			}

			return jsonStrings;
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