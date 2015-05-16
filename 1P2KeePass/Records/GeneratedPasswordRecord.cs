using System;
using System.Collections.Generic;
using System.Text;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
    public class GeneratedPasswordRecord : BaseRecord
    {
        public GeneratedPasswordSecureContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            return null;//TODO
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.Key };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));
            entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, StringExt.GetValueOrEmpty(secureContents.password)));
            entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(location)));

            return entry;
        }
    }

    public class GeneratedPasswordSecureContents
    {
        public string password { get; set; }
    }
}
