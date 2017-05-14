using System;
using System.Collections.Generic;
using System.Text;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
    public class RouterRecord : BaseRecord
    {
        public RouterSecureContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.IRCommunication };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));
            entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.network_name)));
            entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, StringExt.GetValueOrEmpty(secureContents.wireless_password)));
            entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(secureContents.server)));

            if (!String.IsNullOrEmpty(secureContents.network_name))
                entry.Strings.Set("Network name", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.network_name)));

            if (!String.IsNullOrEmpty(secureContents.airport_id))
                entry.Strings.Set("AirPort ID", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.airport_id)));

            if (!String.IsNullOrEmpty(secureContents.wireless_security))
                entry.Strings.Set("Wireless security", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.wireless_security)));

            if (!String.IsNullOrEmpty(secureContents.name))
                entry.Strings.Set("Name of base station", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.name)));

            if (!String.IsNullOrEmpty(secureContents.password))
                entry.Strings.Set("Password of base station", new ProtectedString(true, StringExt.GetValueOrEmpty(secureContents.password)));

            if (!String.IsNullOrEmpty(secureContents.disk_password))
                entry.Strings.Set("Password for network storage", new ProtectedString(true, StringExt.GetValueOrEmpty(secureContents.disk_password)));

            if (!string.IsNullOrEmpty(StringExt.GetValueOrEmpty(secureContents.notesPlain)))
                entry.Strings.Set(PwDefs.NotesField, new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(secureContents.notesPlain)));

            return entry;
        }
    }

    public class RouterSecureContents
    {
        public string name { get; set; }
        public string password { get; set; }
        public string server { get; set; }
        public string airport_id { get; set; }
        public string network_name { get; set; }
        public string wireless_security { get; set; }
        public string wireless_password { get; set; }
        public string disk_password { get; set; }
        public string notesPlain { get; set; }
    }
}