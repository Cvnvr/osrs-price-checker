using System;

namespace OsrsPriceChecker.Common
{
	public class UserInputHandler
	{
		public enum ItemType { Item, Weapon, Equipment };

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
			string categoryMessage = "\nSelect what category the item belongs to ('item', 'weapon', or 'equipment'):";

			// Determine category from user
			Console.WriteLine(categoryMessage);
			string response = Console.ReadLine();

			while (!IsCategoryValid(response))
			{
				Console.WriteLine("That isn't a valid category\n");
				Console.WriteLine(categoryMessage);

				response = Console.ReadLine();
			}

			return SetItemType(response);
		}

		private bool IsCategoryValid(string response)
		{
			if (string.IsNullOrEmpty(response))
			{
				return false;
			}

			string categoryLowered = response.ToLower();
			if (categoryLowered == "item" || categoryLowered == "weapon" || categoryLowered == "equipment")
			{
				return true;
			}

			return false;
		}

		private ItemType SetItemType(string response)
		{
			switch (response.ToLower())
			{
				case "item":
					return ItemType.Item;
				case "weapon":
					return ItemType.Weapon;
				default:
					return ItemType.Equipment;
			}
		}

		private string GetObjectNameFromUser()
		{
			Console.WriteLine("\nEnter the name of the object:");

			return Console.ReadLine();
		}

		public void EndSearchSequence()
		{
			Console.WriteLine("\n\nSearch complete!\n\n");

			string searchConfirmation = "\nSearch for another object? (y/n)";

			Console.WriteLine(searchConfirmation);
			string answer = Console.ReadLine();

			while (!InputIsValid())
			{
				Console.WriteLine("\nInvalid response.");
				Console.WriteLine(searchConfirmation);

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