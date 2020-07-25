using System;

namespace OsrsPriceChecker
{
	class UserInputRecorder
	{
		private static void Main(string[] args)
		{
			Console.WriteLine("\n----------------------------------\n");
			Console.WriteLine("Welcome to the OSRS Price Checker!");
			Console.WriteLine("\n----------------------------------\n");

			// Get required input from user
			ItemType itemType = GetCategoryFromUser();
			string itemName = GetItemNameFromUser();

			Console.WriteLine(string.Format("\nSearching for 'item': {0}...", itemName));

			// Make API call using the user input
			DataRetriever dataRetriever = new DataRetriever();
			dataRetriever.GetItemData(itemType, itemName);
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
			#endregion
		}

		private static string GetItemNameFromUser()
		{
			Console.WriteLine("\n\nEnter the name of the object:");
			return Console.ReadLine();
		}
	}
}