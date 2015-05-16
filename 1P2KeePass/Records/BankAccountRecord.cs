using System;
using System.Collections.Generic;
using System.Text;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
    public class BankAccountRecord : BaseRecord
    {
        public BankAccountSecureContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.Identity };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));
            entry.Strings.Set(PwDefs.UserNameField, new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, StringExt.GetValueOrEmpty(secureContents.owner)));
            entry.Strings.Set("BIC/SWIFT", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.swift)));
            entry.Strings.Set("IBAN", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.iban)));
            entry.Strings.Set("Kontonummer", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.accountNo)));
            entry.Strings.Set("Bankleitzahl", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.routingNo)));
            entry.Strings.Set("Bank", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.bankName)));

            if (!String.IsNullOrEmpty(secureContents.accountType))
                entry.Strings.Set("Konto-Typ", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.accountType)));

            if (!String.IsNullOrEmpty(secureContents.telephonePin))
                entry.Strings.Set("Telefon-PIN", new ProtectedString(true, StringExt.GetValueOrEmpty(secureContents.telephonePin)));

            if (!String.IsNullOrEmpty(secureContents.branchAddress))
                entry.Strings.Set("Filiale", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.branchAddress)));

            if (!String.IsNullOrEmpty(secureContents.branchPhone))
                entry.Strings.Set("Filiale (Telefon)", new ProtectedString(false, StringExt.GetValueOrEmpty(secureContents.branchPhone)));

            if (!string.IsNullOrEmpty(StringExt.GetValueOrEmpty(secureContents.notesPlain)))
                entry.Strings.Set(PwDefs.NotesField, new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(secureContents.notesPlain)));

            return entry;
        }
    }

    public class BankAccountSecureContents
    {
        public string bankName { get; set; }
        public string owner { get; set; }
        public string accountType { get; set; }
        public string routingNo { get; set; }
        public string accountNo { get; set; }
        public string swift { get; set; }
        public string iban { get; set; }
        public string telephonePin { get; set; }
        public string branchPhone { get; set; }
        public string branchAddress { get; set; }
        public string notesPlain { get; set; }
    }
}
