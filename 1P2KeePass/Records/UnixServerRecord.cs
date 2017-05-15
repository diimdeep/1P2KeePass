using System;
using System.Collections.Generic;
using System.Text;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
    public class UnixServerRecord : BaseRecord
    {
        public UnixServerSecureContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.NetworkServer };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));
            entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.username)));
            entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, StringExt.GetValueOrEmpty(secureContents.password)));
            entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(secureContents.url)));

            if (!string.IsNullOrEmpty(StringExt.GetValueOrEmpty(secureContents.notesPlain)))
                entry.Strings.Set(PwDefs.NotesField, new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(secureContents.notesPlain)));

            return entry;
        }
    }

    public class UnixServerSecureContents
    {
        public string url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string notesPlain { get; set; }
    }
}
