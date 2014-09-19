using System;
using System.Collections.Generic;
using KeePassLib;
using KeePassLib.Interfaces;
using KeePassLib.Security;

namespace _1Password2KeePass
{
	public class PIFImporter
	{
		public void Import(List<BaseRecord> baseRecords, PwDatabase storage, IStatusLogger status)
		{
			var records = new List<BaseRecord>();
			var trashedRecords = new List<BaseRecord>();

			baseRecords.ForEach(record =>
			{
				if (record.trashed)
					trashedRecords.Add(record);
				else
					records.Add(record);
			});


			var tree = BuildTree(records);

			status.SetText("Importing records..", LogStatusType.Info);
			
			PwGroup root = new PwGroup(true, true);
			root.Name = "1Password Import on " + DateTime.Now.ToString();


			foreach (var node in tree)
			{
				ImportRecord(node, root, storage);
			}

			if (trashedRecords.Count > 0)
			{
				PwGroup trash = new PwGroup(true, true) { Name = "Trash", IconId = PwIcon.TrashBin };

				foreach (var trecord in trashedRecords)
				{
					var wfrecord = trecord as WebFormRecord;
					if (wfrecord != null)
						CreateWebForm(trash, storage, wfrecord);
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
			else if (record.GetType() == typeof(WebFormRecord))
			{
				WebFormRecord webForm = (WebFormRecord)record;
				CreateWebForm(groupAddTo, pwStorage, webForm);
			}
			else if (record.GetType() == typeof(BaseRecord))
			{
				//Trace.WriteLine(String.Format("Error. Can't import unknown record type: {0}", record.RawJson));
			}
			else if (record.GetType() == typeof(UnknownRecord))
			{
				//CreateUnknown(groupAddTo, pwStorage, record as UnknownRecord);
			}
		}

		private static void CreateWebForm(PwGroup groupAddTo, PwDatabase pwStorage, WebFormRecord webForm)
		{
			PwEntry entry = new PwEntry(true, true);

			entry.CreationTime = DateTimeExt.FromUnixTimeStamp(webForm.createdAt);

			entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(webForm.updatedAt);
			
			entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, webForm.title));
			entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, webForm.location));
			entry.Strings.Set(PwDefs.UserNameField,
				new ProtectedString(pwStorage.MemoryProtection.ProtectUserName, webForm.secureContents.GetUsername()));
			entry.Strings.Set(PwDefs.PasswordField,
				new ProtectedString(pwStorage.MemoryProtection.ProtectPassword, webForm.secureContents.GetPassword()));
			if (!string.IsNullOrEmpty(webForm.secureContents.notesPlain))
				entry.Strings.Set(PwDefs.NotesField,
					new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, webForm.secureContents.notesPlain));
			
			groupAddTo.AddEntry(entry, true);
		}

		private static void CreateUnknown(PwGroup groupAddTo, PwDatabase pwStorage, UnknownRecord record)
		{
			PwEntry entry = new PwEntry(true, true) { IconId = PwIcon.Warning };
			entry.CreationTime = DateTimeExt.FromUnixTimeStamp(record.createdAt);
			entry.LastModificationTime = DateTimeExt.FromUnixTimeStamp(record.updatedAt);
			entry.Strings.Set(PwDefs.TitleField, new ProtectedString(pwStorage.MemoryProtection.ProtectTitle, record.title));
			entry.Strings.Set(PwDefs.UrlField, new ProtectedString(pwStorage.MemoryProtection.ProtectUrl, record.location));
			entry.Strings.Set(PwDefs.NotesField,
				new ProtectedString(pwStorage.MemoryProtection.ProtectNotes, record.typeName));
		}

		private static PwGroup CreateFolder(PwGroup groupAddTo, FolderRecord folderRecord)
		{
			PwGroup folder = new PwGroup(true, true);
			folder.Name = folderRecord.title;
			folder.CreationTime = DateTimeExt.FromUnixTimeStamp(folderRecord.createdAt);
			folder.LastModificationTime = DateTimeExt.FromUnixTimeStamp(folderRecord.updatedAt);
			groupAddTo.AddGroup(folder, true);
			return folder;
		}
	}
}