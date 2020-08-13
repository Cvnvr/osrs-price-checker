using System;
using System.Collections.Generic;
using System.Text;

namespace OsrsPriceChecker.Connector
{
	public static class EndPoints
	{
		// Items
		public const string AllItemsEndPoint = "https://raw.githubusercontent.com/osrsbox/osrsbox-db/master/docs/items-search.json";
		public const string ItemEndPoint = "https://api.osrsbox.com/items";

		// Weapons
		public const string AllWeaponsEndPoint = "";
		public const string WeaponEndPoint = "https://api.osrsbox.com/weapons";

		// Equipment
		public const string AllEquipmentEndPoint = "";
		public const string EquipmentEndPoint = "https://api.osrsbox.com/equipment";
	}
}