using Newtonsoft.Json;
using OsrsPriceChecker.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace OsrsPriceChecker.Connector
{
	public class WebAPI
	{
		public List<CoreItemData> FetchCoreItemData()
		{
			string jsonString = MakeHttpRequest(EndPoints.AllItemsEndPoint);
			if (string.IsNullOrEmpty(jsonString))
			{
				return null;
			}

			// Parse json string into data object
			Dictionary<string, CoreItemData> myDict = JsonConvert.DeserializeObject<Dictionary<string, CoreItemData>>(jsonString);
			List<CoreItemData> allItems = new List<CoreItemData>(myDict.Values);

			// Remove duplicate elements
			allItems.RemoveAll(item => item.Duplicate);

			return allItems;
		}

		public List<string> ReturnFilteredItemResults(List<CoreItemData> filteredData)
		{
			List<string> jsonStrings = new List<string>();

			for (int i = 0; i < filteredData.Count; i++)
			{
				string endPoint = $"{EndPoints.ItemEndPoint}/{filteredData[i].Id}";
				jsonStrings.Add(MakeHttpRequest(endPoint));
			}

			return jsonStrings;
		}

		private string MakeHttpRequest(string endPoint)
		{
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
		}
	}
}