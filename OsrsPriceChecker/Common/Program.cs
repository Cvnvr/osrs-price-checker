using System;

namespace OsrsPriceChecker
{
	public class Program
	{
		public static WebAPI webAPI;
		public static DataRetriever dataRetriever;

		private static void Main(string[] args)
		{
			InitialiseScriptReferences();

			Console.WriteLine("\n----------------------------------\n");
			Console.WriteLine("Welcome to the OSRS Price Checker!");
			Console.WriteLine("\n----------------------------------\n");

			GetAllItems();

			// Get required input from user
			ItemType itemType = GetCategoryFromUser();
			string itemName = GetItemNameFromUser();

			// Make API call using the user input
			dataRetriever.ParseItemData(itemName);
		}

		private static void InitialiseScriptReferences()
		{
			webAPI = new WebAPI();
			dataRetriever = new DataRetriever();
		}

		private static async void GetAllItems()
		{
			Console.WriteLine("Retrieving item data from API...\n");

			await webAPI.GetCoreItemData();

			Console.WriteLine("Done!\n");
		}

		private static ItemType GetCategoryFromUser()
		{
			// Determine category from user
			Console.WriteLine("\n\nSelect what category the item belongs to ('item', 'weapon', or 'equipment'):");
			string category = Console.ReadLine();
			while (!IsCategoryValid())
			{
				Console.WriteLine("That isn't a valid category\n");
				Console.WriteLine("Select what category the item belongs to ('item', 'weapon', or 'equipment'):");
				category = Console.ReadLine();
			}

			return SetItemType();

			#region Local Functions
			bool IsCategoryValid()
			{
				string categoryLowered = category.ToLower();
				if (categoryLowered == "item" || categoryLowered != "weapon" || categoryLowered != "equipment")
				{
					return true;
				}

				return false;
			}

			ItemType SetItemType()
			{
				switch (category.ToLower())
				{
					case "item":
						return ItemType.Item;
					case "weapon":
						return ItemType.Weapon;
					default:
						return ItemType.Equipment;
				}
			}
			#endregion Local Functions
		}

		private static string GetItemNameFromUser()
		{
			Console.WriteLine("\n\nEnter the name of the object:");

			return Console.ReadLine();

			// Format string ('example STRING Here' -> 'Example string here')

			// return Helpers.Helper.FirstLetterToUpperCase(Console.ReadLine());
		}
	}
}