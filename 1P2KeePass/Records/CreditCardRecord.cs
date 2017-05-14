using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
	public class CreditCardRecord : BaseRecord
	{
		public CreditCardSecuteContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.Money };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));
            entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.cardholder)));
            entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, StringExt.GetValueOrEmpty(secureContents.ccnum)));
            entry.Strings.Set("Verification code", new ProtectedString(true, StringExt.GetValueOrEmpty(secureContents.cvv)));
            entry.Strings.Set("Date of Expiry", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.expiry_mm).PadLeft(2, '0') + " / " + StringExt.GetValueOrEmpty(secureContents.expiry_yy).PadLeft(4, '0')));
            entry.Strings.Set("Bank", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.bank)));

            if (!String.IsNullOrEmpty(secureContents.phoneIntl))
                entry.Strings.Set("Phone", new ProtectedString(false, secureContents.phoneIntl));

            if (!string.IsNullOrEmpty(StringExt.GetValueOrEmpty(secureContents.notesPlain)))
                entry.Strings.Set(PwDefs.NotesField, new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(secureContents.notesPlain)));

            return entry;
        }
	}

	public class CreditCardSecuteContents
	{
		public string expiry_yy { get; set; }
		public string expiry_mm { get; set; }
		public string cardholder { get; set; }
		public string website { get; set; }
		public string bank { get; set; }
		public string type { get; set; }
		public string ccnum { get; set; }
        public string cvv { get; set; }
        public string phoneIntl { get; set; }
        public string notesPlain { get; set; }

		// todo sections
	}
}