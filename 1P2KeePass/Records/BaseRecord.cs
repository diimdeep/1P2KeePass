using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using KeePassLib;

namespace _1Password2KeePass
{
	public class BaseRecord
	{
		[JsonProperty("uuid")]
		public string ID { get; set; }

		public string folderUuid { get; set; }

		[JsonProperty("locationLKey")]
		public string locationKey { get; set; }
		
		public string location { get; set; }		
		
		public long updatedAt { get; set; }

		public long createdAt { get; set; }

		public string title { get; set; }

		public bool trashed { get; set; }

		public string securityLevel { get; set; }
		
		public string contentsHash { get; set; }

        public virtual PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            return null;
        }
	}
}