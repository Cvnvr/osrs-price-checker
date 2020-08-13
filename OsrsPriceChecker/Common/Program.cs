using OsrsPriceChecker.Connector;
using System;

namespace OsrsPriceChecker.Common
{
	public enum ItemType { Item, Weapon, Equipment };

	public class Program
	{
		public static DataRetriever dataRetriever;
		public static UserInputHandler userInputHandler;

		private static void Main()
		{
			InitialiseObjects();

			DisplayWelcomeMessage();

			dataRetriever.FetchCoreData();
			userInputHandler.RetrieveUserInput();
		}

		private static void InitialiseObjects()
		{
			dataRetriever = new DataRetriever();
			userInputHandler = new UserInputHandler();
		}

		private static void DisplayWelcomeMessage()
		{
			Console.WriteLine("\n----------------------------------\n");
			Console.WriteLine("Welcome to the OSRS Price Checker!");
			Console.WriteLine("\n----------------------------------\n");
		}
	}
}