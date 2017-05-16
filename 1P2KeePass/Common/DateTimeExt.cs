using System;

namespace _1Password2KeePass
{
	static class DateTimeExt
	{
		public static DateTime FromUnixTimeStamp(long unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

            // fix bug when timestamp is millis instead of seconds [issues/2]
            if (unixTimeStamp > 20000000000)			
				dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();			
			else
				dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}
	}
}
