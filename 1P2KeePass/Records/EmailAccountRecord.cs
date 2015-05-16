using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
    public class EmailAccountRecord : BaseRecord
    {
        public EmailAccountSecureContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.EMail };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));
            entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.pop_username)));
            entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, StringExt.GetValueOrEmpty(secureContents.pop_password)));
            entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(secureContents.provider_website)));

            if (!String.IsNullOrEmpty(secureContents.pop_type))
                entry.Strings.Set("POP Typ", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.pop_security)));

            if (!String.IsNullOrEmpty(secureContents.pop_server))
                entry.Strings.Set("POP Server", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.pop_server)));

            if (!String.IsNullOrEmpty(secureContents.pop_port))
                entry.Strings.Set("POP Port", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.pop_port)));

            if (!String.IsNullOrEmpty(secureContents.pop_security))
                entry.Strings.Set("POP Sicherheit", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.pop_security)));

            if (!String.IsNullOrEmpty(secureContents.pop_authentication))
                entry.Strings.Set("POP Authentifizierung", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.pop_authentication)));

            if (!String.IsNullOrEmpty(secureContents.smtp_server))
                entry.Strings.Set("SMTP Server", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.smtp_server)));

            if (!String.IsNullOrEmpty(secureContents.smtp_port))
                entry.Strings.Set("SMTP Port", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.smtp_port)));

            if (!String.IsNullOrEmpty(secureContents.smtp_security))
                entry.Strings.Set("SMTP Sicherheit", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.smtp_security)));

            if (!String.IsNullOrEmpty(secureContents.smtp_authentication))
                entry.Strings.Set("SMTP Authentifizierung", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.smtp_authentication)));

            if (!String.IsNullOrEmpty(secureContents.smtp_username))
                entry.Strings.Set("SMTP Benutzername", new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.smtp_username)));

            if (!String.IsNullOrEmpty(secureContents.smtp_password))
                entry.Strings.Set("SMTP Passwort", new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, StringExt.GetValueOrEmpty(secureContents.smtp_password)));

            if (!String.IsNullOrEmpty(secureContents.provider))
                entry.Strings.Set("Anbieter", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.provider)));

            return entry;
        }
    }

    public class EmailAccountSecureContents
    {
        public string pop_type { get; set; }
        public string pop_server { get; set; }
        public string pop_port { get; set; }
        public string pop_security { get; set; }
        public string pop_authentication { get; set; }
        public string pop_username { get; set; }
        public string pop_password { get; set; }
        public string smtp_server { get; set; }
        public string smtp_port { get; set; }
        public string smtp_security { get; set; }
        public string smtp_authentication { get; set; }
        public string smtp_username { get; set; }
        public string smtp_password { get; set; }
        public string provider { get; set; }
        public string provider_website { get; set; }
        public string notesPlain { get; set; }
    }
}