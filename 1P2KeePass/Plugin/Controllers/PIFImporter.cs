using System;
using System.Collections.Generic;
using System.Diagnostics;
using KeePassLib;
using KeePassLib.Interfaces;
using KeePassLib.Security;

namespace _1Password2KeePass
{
    public class subfolder
    {
        public subfolder() { }

        public String folderName { get; set; }
        public PwIcon folderIcon { get; set; }
        public List<BaseRecord> recordList { get; set; }
    }

	public class PIFImporter
	{
        public void Import(List<BaseRecord> baseRecords, PwDatabase storage, IStatusLogger status, bool createSubfolders = false)
		{

        var loginRecords = new List<BaseRecord>();
            var walletRecords = new List<BaseRecord>();
            var accountsRecords = new List<BaseRecord>();
            var softwareRecords = new List<BaseRecord>();
            var secureNoteRecords = new List<BaseRecord>();
            var identityRecords = new List<BaseRecord>();
            var RecordsList = new List<subfolder>();

            var records = new List<BaseRecord>();
            var trashedRecords = new List<BaseRecord>();

            if ( createSubfolders)
            {
                RecordsList.Add(new subfolder { folderName = "Logins", folderIcon = PwIcon.World, recordList = loginRecords });
                RecordsList.Add(new subfolder { folderName = "Wallet", folderIcon = PwIcon.Money, recordList = walletRecords });
                RecordsList.Add(new subfolder { folderName = "Accounts", folderIcon = PwIcon.NetworkServer, recordList = accountsRecords });
                RecordsList.Add(new subfolder { folderName = "Software", folderIcon = PwIcon.MultiKeys, recordList = softwareRecords });
                RecordsList.Add(new subfolder { folderName = "Secure Notes", folderIcon = PwIcon.Notepad, recordList = secureNoteRecords });
                RecordsList.Add(new subfolder { folderName = "identites", folderIcon = PwIcon.Identity, recordList = identityRecords });
            }

            baseRecords.ForEach(record =>
			{
				if (record.trashed)
                {
					trashedRecords.Add(record);
                }
				else
                {
                    if (!createSubfolders)
                    {
                        records.Add(record);
                    }
                    else
                    { 
                        if (record.GetType() == typeof(WebFormRecord))
                        {
                            loginRecords.Add(record);
                        }
                        else if (  record.GetType() == typeof(BankAccountRecord)
                                || record.GetType() == typeof(CreditCardRecord)
                                || record.GetType() == typeof(MembershipRecord)
                                || record.GetType() == typeof(SocialSecurityNumberRecord) )
                        {
                            walletRecords.Add(record);
                        }
                        else if (  record.GetType() == typeof(DatabaseConnectionRecord)
                                || record.GetType() == typeof(EmailAccountRecord)
                                || record.GetType() == typeof(FtpAccountRecord)
                                || record.GetType() == typeof(GenericAccountRecord)
                                || record.GetType() == typeof(UnixServerRecord)
                                || record.GetType() == typeof(RouterRecord) )
                        {
                            accountsRecords.Add(record);
                        }
                        else if (record.GetType() == typeof(ComputerLicenseRecord))
                        {
                            softwareRecords.Add(record);
                        }
                        else if (record.GetType() == typeof(SecureNoteRecord))
                        {
                            secureNoteRecords.Add(record);
                        }
                        else if (record.GetType() == typeof(IndentityRecord))
                        {
                            identityRecords.Add(record);
                        }
                    }
                }
			});

			status.SetText("Importing records..", LogStatusType.Info);
			PwGroup root = new PwGroup(true, true);
			root.Name = "1Password Import on " + DateTime.Now.ToString();

            if ( createSubfolders)
            {
                foreach ( var category in RecordsList)
                {
                    PwGroup categoryGroup = new PwGroup(true, true);
                    categoryGroup.Name = category.folderName;
                    categoryGroup.IconId = category.folderIcon;

                    var tree = BuildTree(category.recordList);
			        foreach (var node in tree)
			        {
				        ImportRecord(node, categoryGroup, storage);
			        }
                    root.AddGroup(categoryGroup, true);
                }
            }
            else
            {
                var tree = BuildTree(records);
                foreach (var node in tree)
                {
                    ImportRecord(node, root, storage);
                }
            }

			if (trashedRecords.Count > 0)
			{
				PwGroup trash = new PwGroup(true, true) { Name = "Trash", IconId = PwIcon.TrashBin };

				foreach (var trecord in trashedRecords)
				{
					var wfrecord = trecord as WebFormRecord;
					if (wfrecord != null)
                    {
                        PwEntry entry = wfrecord.CreatePwEntry(storage);

                        if (entry != null)
                            trash.AddEntry(entry, true);
                    }
				}
				root.AddGroup(trash, true);
			}

			storage.RootGroup.AddGroup(root, true);
		}

		IEnumerable<Node<BaseRecord>> BuildTree(List<BaseRecord> records)
		{
			Dictionary<string, Node<BaseRecord>> lookup = new Dictionary<string, Node<BaseRecord>>();
			records.ForEach(x => lookup.Add(x.ID, new Node<BaseRecord>() { AssociatedObject = x }));

			foreach (Node<BaseRecord> node in lookup.Values)
			{
				Node<BaseRecord> proposedParent;
				if (!string.IsNullOrEmpty(node.AssociatedObject.folderUuid))
					if (lookup.TryGetValue(node.AssociatedObject.folderUuid, out proposedParent))
					{
						node.Parent = proposedParent;
						proposedParent.Nodes.Add(node);
					}
			}

			List<Node<BaseRecord>> rootNodes = new List<Node<BaseRecord>>();
			foreach (var value in lookup.Values)
			{
				if (value.Parent == null)
					rootNodes.Add(value);
			}
			return rootNodes;
		}

		private static void ImportRecord(Node<BaseRecord> currentNode, PwGroup groupAddTo, PwDatabase pwStorage)
		{
			BaseRecord record = currentNode.AssociatedObject;

			if (record.GetType() == typeof(FolderRecord))
			{
				FolderRecord folderRecord = (FolderRecord)record;
				var folder = CreateFolder(groupAddTo, folderRecord);

				foreach (var node in currentNode.Nodes)
				{
					ImportRecord(node, folder, pwStorage);
				}
			}
            else if (record.GetType() == typeof(GeneratedPasswordRecord))
            {
                // Don't import generated passwords - these are just generated passwords without a username
            }
            /*
            else if (record.GetType() == typeof(BaseRecord))
            {
                //Trace.WriteLine(String.Format("Error. Can't import unknown record type: {0}", record.RawJson));
            }
            else if (record.GetType() == typeof(UnknownRecord))
            {
                //CreateUnknown(groupAddTo, pwStorage, record as UnknownRecord);
            }
            */
            else
            {
                // Let the record create a password entry:
                PwEntry entry = record.CreatePwEntry(pwStorage);

                // If the record has created an entry, then add it to the group. If no entry was returned, then that record type is not supported yet.
                if (entry != null)
                    groupAddTo.AddEntry(entry, true);
                //else
                //    Trace.WriteLine("Entry could not be imported (did not return a valid entry): " + record.GetType().Name);
            }
		}

		private static void CreateUnknown(PwGroup groupAddTo, PwDatabase pwStorage, UnknownRecord record)
		{
			PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.Warning };
			entry.CreationTime = DateTimeExt.FromUnixTimeStamp(record.createdAt);
			entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(record.updatedAt);
			entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, StringExt.GetValueOrEmpty(record.title)));
			entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, StringExt.GetValueOrEmpty(record.location)));
			entry.Strings.Set(PwDefs.NotesField,
				new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, StringExt.GetValueOrEmpty(record.typeName)));
		}

		private static PwGroup CreateFolder(PwGroup groupAddTo, FolderRecord folderRecord)
		{
			PwGroup folder = new PwGroup(true, true);
			folder.Name = StringExt.GetValueOrEmpty(folderRecord.title);
			folder.CreationTime = DateTimeExt.FromUnixTimeStamp(folderRecord.createdAt);
			folder.LastModificationTime = DateTimeExt.FromUnixTimeStamp(folderRecord.updatedAt);
			groupAddTo.AddGroup(folder, true);
			return folder;
		}
    }
}