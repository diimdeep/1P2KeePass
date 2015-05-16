using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
	public class WebFormRecord : BaseRecord
	{
		[JsonProperty("secureContents")]
		public WebFormSecureContents secureContents { get; set; }
		public WebFormOpenContents openContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.World };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));
            entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(location)));
            entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, secureContents.GetUsername()));
            entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, secureContents.GetPassword()));

            if (!string.IsNullOrEmpty(StringExt.GetValueOrEmpty(secureContents.notesPlain)))
                entry.Strings.Set(PwDefs.NotesField, new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(secureContents.notesPlain)));

            return entry;
        }
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
			if (Fields != null)
				foreach (var field in Fields)
				{
					if (field.designation == "username")
						return StringExt.GetValueOrEmpty(field.value);
				}
			return String.Empty;
		}

		public  string GetPassword()
		{
			if (Fields != null)
				foreach (var field in Fields)
				{
					if (field.designation == "password")
						return StringExt.GetValueOrEmpty(field.value);
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
