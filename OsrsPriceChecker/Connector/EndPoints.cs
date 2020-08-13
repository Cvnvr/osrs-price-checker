namespace OsrsPriceChecker.Connector
{
	public static class EndPoints
	{
		public const string AllItems = "https://raw.githubusercontent.com/osrsbox/osrsbox-db/master/docs/items-search.json";

		// Individual search end points
		public const string ItemSearch = "https://api.osrsbox.com/items";
		public const string WeaponSearch = "https://api.osrsbox.com/weapons";
		public const string EquipmentSearch = "https://api.osrsbox.com/equipment";
	}
}