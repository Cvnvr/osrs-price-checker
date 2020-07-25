using System;

namespace OsrsPriceChecker
{
	class Program
	{
		#region Variables
		private static ItemType type;
		#endregion Variables

		private static void Main(string[] args)
		{
			// string userInput = GetUserInput();

			type = ItemType.Item;
			string userInput = "";

			DataRetriever dataRetriever = new DataRetriever();
			dataRetriever.MakeTheRequest(type, userInput);
		}

		private static string GetUserInput()
		{
			Console.WriteLine("Welcome to the OSRS Price Checker!\n\n");

			Console.WriteLine("Select what category the item belongs to:");
			string category = Console.ReadLine();
			// TODO validate against existing categories

			Console.WriteLine("Enter the name of the object:");
			string input = Console.ReadLine();
			// TODO read user input

			return input;
		}
	}
}