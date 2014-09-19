using System;

static internal class StringExt
{
	public static string GetValueOrEmpty(string str)
	{
		if (String.IsNullOrEmpty(str)) return String.Empty;
		return str;
	}
}