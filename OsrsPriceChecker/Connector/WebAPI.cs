using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OsrsPriceChecker
{
	public enum ItemType { Item, Weapon, Equipment };

	public class WebAPI
	{
		#region Variables
		private readonly string itemEndPoint = "https://api.osrsbox.com/items";
		private readonly string weaponEndPoint = "https://api.osrsbox.com/weapons";
		private readonly string equipmentEndPoint = "https://api.osrsbox.com/equipment";
		#endregion Variables

		public async Task<string> HttpRequest(ItemType itemType, string userInput)
		{
			string endPoint = GetCorrespondingEndPoint() + FormatSearchParamater(userInput);

			// Contact end point
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(endPoint);
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

		private string FormatSearchParamater(string userInput)
		{
			string formattedInput = Helpers.Helper.FirstLetterToUpperCase(userInput);

			// Validate where there are multiple words
			if (!formattedInput.Contains(' '))
			{
				return string.Format("?where={\"name\":\"{0}\"}", formattedInput);
			}

			string[] words = formattedInput.Split(' ');
			string userQuery = "";

			for (int i = 0; i < words.Length; i++)
			{
				if (i == 0)
				{
					userQuery = words[i];
					continue;
				}

				userQuery += string.Format("%20{0}", words[i]);
			}

			return string.Format("?where={\"name\":\"{0}\"}", userQuery);
		}
	}
}