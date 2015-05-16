using System;
using System.Collections.Generic;
using System.Text;
using KeePassLib;
using KeePassLib.Security;

namespace _1Password2KeePass
{
	public class SecureNoteRecord : BaseRecord
	{
		public SecureNoteSecuteContents secureContents { get; set; }

        public override PwEntry CreatePwEntry(PwDatabase pwStorage)
        {
            PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.Note };

            entry.CreationTime = DateTimeExt.FromUnixTimeStamp(createdAt);

            entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(updatedAt);

            entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(title)));

            if (!string.IsNullOrEmpty(StringExt.GetValueOrEmpty(secureContents.notesPlain)))
                entry.Strings.Set(PwDefs.NotesField,
                    new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(secureContents.notesPlain)));

            return entry;
        }
    }

	public class SecureNoteSecuteContents
	{
		public string notesPlain { get; set; }
	}
}