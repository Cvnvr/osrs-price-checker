using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OsrsPriceChecker
{
	public enum ItemType { Item, Weapon, Equipment };

	class WebAPI
	{
		#region Variables
		private readonly string itemEndPoint = "https://api.osrsbox.com/items";
		private readonly string weaponEndPoint = "https://api.osrsbox.com/weapons";
		private readonly string equipmentEndPoint = "https://api.osrsbox.com/equipment";
		#endregion Variables

		public async Task<string> HttpRequest(ItemType itemType, string userInput)
		{
			// Contact end point
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(GetCorrespondingEndPoint());
			webRequest.Method = "GET";

			HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

			// Parse response into JSON
			string jsonString;
			using (Stream stream = webResponse.GetResponseStream())
			{
				StreamReader reader = new StreamReader(stream, Encoding.UTF8);
				jsonString = reader.ReadToEnd();
			}

			return jsonString;

			#region Local Functions
			string GetCorrespondingEndPoint()
			{
				switch (itemType)
				{
					case ItemType.Item:
						return itemEndPoint;
					case ItemType.Weapon:
						return weaponEndPoint;
					case ItemType.Equipment:
						return equipmentEndPoint;
					default:
						return "ERROR";
				}
			}
			#endregion Local Functions
		}
	}
}