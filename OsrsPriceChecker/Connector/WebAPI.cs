using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace OsrsPriceChecker.Connector
{
	public static class WebAPI
	{
		public static string MakeHttpRequest(string uri)
		{
			string jsonString = "";

			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
				request.Method = "GET";

				HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				using (Stream stream = response.GetResponseStream())
				{
					StreamReader reader = new StreamReader(stream, Encoding.UTF8);
					jsonString = reader.ReadToEnd();
				}
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine("\nException Caught!");
				Console.WriteLine("Message :{0} ", e.Message);
			}

			return jsonString;
		}
	}
}