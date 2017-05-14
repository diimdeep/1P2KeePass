using System;
using System.Collections.Generic;
using System.Text;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
	public class ComputerLicenseRecord : BaseRecord
	{
		public ComputerLicenseSecuteContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.Package };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));

            if (!String.IsNullOrEmpty(secureContents.reg_name))
            {
                entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.reg_name)));

                if (!String.IsNullOrEmpty(secureContents.reg_email))
                    entry.Strings.Set("Registration E-mail", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.reg_email)));
            }
            else
                entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.reg_email)));

            entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, StringExt.GetValueOrEmpty(secureContents.reg_code)));

            if (!String.IsNullOrEmpty(secureContents.download_link))
            {
                entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(secureContents.download_link)));

                if (!String.IsNullOrEmpty(secureContents.publisher_website))
                    entry.Strings.Set("Publisher website", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.publisher_website)));
            }
            else if (!String.IsNullOrEmpty(secureContents.publisher_website))
                entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(secureContents.publisher_website)));

            entry.Strings.Set("Publisher", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.publisher_name)));
            entry.Strings.Set("Version", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.product_version)));
            entry.Strings.Set("Support E-mail", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.support_email)));
            entry.Strings.Set("Order number", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.order_number)));

            if (!string.IsNullOrEmpty(StringExt.GetValueOrEmpty(secureContents.notesPlain)))
                entry.Strings.Set(PwDefs.NotesField, new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(secureContents.notesPlain)));

            return entry;
        }
    }

	public class ComputerLicenseSecuteContents
	{
		public string reg_name { get; set; }
        public string reg_email { get; set; }
		public string reg_code { get; set; }
        public string product_version { get; set; }
        public string download_link { get; set; }
        public string publisher_name { get; set; }
        public string publisher_website { get; set; }
        public string support_email { get; set; }
        public string order_number { get; set; }
        public string notesPlain { get; set; }

		// todo sections
	}
}