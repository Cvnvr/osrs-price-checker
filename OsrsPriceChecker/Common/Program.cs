using OsrsPriceChecker.Connector;
using System;

namespace OsrsPriceChecker.Common
{
	public enum ItemType { Item, Weapon, Equipment };

	public class Program
	{
		public static DataRetriever dataRetriever;
		public static SearchHandler searchHandler;
		public static WebAPI webAPI;

		private static void Main(string[] args)
		{
			InitialiseFactories();
			dataRetriever.FetchCoreData();

			Console.WriteLine("\n----------------------------------\n");
			Console.WriteLine("Welcome to the OSRS Price Checker!");
			Console.WriteLine("\n----------------------------------\n");

			searchHandler.InitialiseSearch();
		}

		private static void InitialiseFactories()
		{
			dataRetriever = new DataRetriever();
			searchHandler = new SearchHandler();
			webAPI = new WebAPI();
		}
	}
}