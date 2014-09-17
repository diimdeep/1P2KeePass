using System.Collections.Generic;
using Newtonsoft.Json;

namespace _1Password2KeePass
{
	public class IndentityRecord : BaseRecord
	{
		[JsonProperty("secureContents")]
		public IndentitySecureContents secureContents { get; set; }
	}

	public class IndentitySecureContents
	{
		public List<IndentitySection> sections { get; set; }

		public string username { get; set; }
		public string reminderq { get; set; }
		public string lastname { get; set; }
		public string zip { get; set; }
		public string birthdate_dd { get; set; }
		public string homephone { get; set; }
		public string firstname { get; set; }
		public string country { get; set; }
		public string birthdate_mm { get; set; }
		public string sex { get; set; }
		public string cellphone { get; set; }
		public byte[] customIcon { get; set; }
		public string address1 { get; set; }
		public string city { get; set; }
		public string cellphone_local { get; set; }
		public string state { get; set; }
		public string occupation { get; set; }
		public string birthdate_yy { get; set; }
		public string homephone_local { get; set; }
		public string defphone_local { get; set; }
		public string remindera { get; set; }
		public string defphone { get; set; }
	}

	public class IndentitySection
	{
		public List<IdentityField> fields { get; set; }

		public string title { get; set; }
		public string name { get; set; }
	}

	public class IdentityField
	{
		public string k { get; set; }
		public string v { get; set; }
		public string n { get; set; }
		public string t { get; set; }
	}
}