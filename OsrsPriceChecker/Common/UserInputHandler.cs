using System;

namespace OsrsPriceChecker.Common
{
	public class UserInputHandler
	{
		public void RetrieveUserInput()
		{
			// Get required input from user
			ItemType itemType = GetCategoryFromUser();
			string itemName = GetObjectNameFromUser();

			switch (itemType)
			{
				case ItemType.Item:
					Program.dataRetriever.ParseItemData(itemName);
					break;
				case ItemType.Weapon:
					// TODO
					break;
				default:
					// TODO
					break;
			}
		}

		private ItemType GetCategoryFromUser()
		{
			// Determine category from user
			Console.WriteLine("\nSelect what category the item belongs to ('item', 'weapon', or 'equipment'):");
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
				if (string.IsNullOrEmpty(category))
				{
					return false;
				}

				string categoryLowered = category.ToLower();
				if (categoryLowered == "item" || categoryLowered == "weapon" || categoryLowered == "equipment")
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

		private string GetObjectNameFromUser()
		{
			Console.WriteLine("\nEnter the name of the object:");

			return Console.ReadLine();
		}

		public void EndSearchSequence()
		{
			Console.WriteLine("\n\nSearch complete!\n\n");

			Console.WriteLine("\nSearch for another object? (y/n)");
			string answer = Console.ReadLine();
			while (!InputIsValid())
			{
				Console.WriteLine("\nInvalid response.");
				Console.WriteLine("\nSearch for another object? (y/n)");
				answer = Console.ReadLine();
			}

			if (answer.ToLower() == "y")
			{
				RetrieveUserInput();
			}
			else
			{
				Environment.Exit(0);
			}

			#region Local Functions
			bool InputIsValid()
			{
				if (string.IsNullOrEmpty(answer))
				{
					return false;
				}

				string answerLowered = answer.ToLower();
				if (answerLowered == "y" || answerLowered == "n")
				{
					return true;
				}

				return false;
			}
			#endregion Local Functions
		}
	}
}