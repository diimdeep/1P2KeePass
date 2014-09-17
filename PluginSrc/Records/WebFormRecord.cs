using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _1Password2KeePass
{
	public class WebFormRecord : BaseRecord
	{
		[JsonProperty("secureContents")]
		public WebFormSecureContents secureContents { get; set; }
		public WebFormOpenContents openContents { get; set; }
	}

	public class WebFormOpenContents
	{
		public string autosubmit { get; set; }
		public string usernameHash { get; set; }
	}

	public class WebFormSecureContents
	{
		[JsonProperty("URLs")]
		public List<WebFormURL> URLs { get; set; }
		[JsonProperty("Fields")]
		public List<WebFormField> Fields { get; set; }
		public string htmlName { get; set; }
		public string htmlMethod { get; set; }
		public string htmlAction { get; set; }
		public string htmlID { get; set; }
		public string notesPlain { get; set; }

		public List<PasswordHistoryRecord> passwordHistory { get; set; }

		public  string GetUsername()
		{
			foreach (var field in Fields)
			{
				if (field.designation == "username")
					return field.value;
			}
			return String.Empty;
		}

		public  string GetPassword()
		{
			foreach (var field in Fields)
			{
				if (field.designation == "password")
					return field.value;
			}
			return String.Empty;
		}
	}

	public class PasswordHistoryRecord
	{
		public string value { get; set; }
		public long time { get; set; }
	}

	public class WebFormField
	{
		public string value { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public string designation { get; set; }
	}

	public class WebFormURL
	{
		public string url { get; set; }
	}
}
