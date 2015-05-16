using System;
using System.Collections.Generic;
using System.Text;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
    public class MembershipRecord : BaseRecord
    {
        public MembershipSecureContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.Identity };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));
            entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.member_name)));
            entry.Strings.Set(PwDefs.PasswordField, new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, StringExt.GetValueOrEmpty(secureContents.membership_no)));
            entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(secureContents.website)));
            entry.Strings.Set("Mitglied seit", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.member_since_mm).PadLeft(2, '0') + " / " + StringExt.GetValueOrEmpty(secureContents.member_since_yy).PadLeft(4, '0')));
            entry.Strings.Set("Organisation", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.org_name)));

            if (!string.IsNullOrEmpty(StringExt.GetValueOrEmpty(secureContents.notesPlain)))
                entry.Strings.Set(PwDefs.NotesField, new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(secureContents.notesPlain)));

            return entry;
        }
    }

    public class MembershipSecureContents
    {
        public string org_name { get; set; }
        public string member_name { get; set; }
        public string membership_no { get; set; }
        public string member_since_mm { get; set; }
        public string member_since_yy { get; set; }
        public string website { get; set; }
        public string notesPlain { get; set; }
    }
}
