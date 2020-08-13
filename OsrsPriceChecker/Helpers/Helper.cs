using System;

namespace OsrsPriceChecker.Helpers
{
	public static class Helper
	{
		/// <summary>
		///    Returns the input string with the first character converted to uppercase
		/// </summary>
		public static string FirstLetterToUpperCase(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentException("There is no first letter");
			}

			str = str.ToLower();
			char[] chars = str.ToCharArray();
			chars[0] = char.ToUpper(chars[0]);

			return new string(chars);
		}
	}
}