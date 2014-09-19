using System.Collections.Generic;
using System.IO;
using KeePass.DataExchange;
using KeePassLib;
using KeePassLib.Interfaces;

namespace _1Password2KeePass
{
	public class _1PasswordPifFormatProvider : FileFormatProvider
	{
		private readonly PIFParser _pifParser = new PIFParser();
		private readonly PIFImporter _pifImporter = new PIFImporter();

		public override bool SupportsImport
		{
			get { return true; }
		}

		public override bool SupportsExport
		{
			get { return false; }
		}

		public override string FormatName
		{
			get { return "1Password Interchange Format"; }
		}

		public override string ApplicationGroup
		{
			get { return KeePass.Resources.KPRes.PasswordManagers; }
		}

		public override string DefaultExtension
		{
			get { return "1pif"; }
		}

		public override bool ImportAppendsToRootGroupOnly
		{
			get { return false; }
		}
		
		public override bool RequiresFile
		{
			get { return true; }
		}

		public override System.Drawing.Image SmallIcon
		{
			get { return _1Password2KeePass.Resources._1P4_Mac_icon; }
		}

		public override void Import(PwDatabase storage, Stream input, IStatusLogger status)
		{
			status.SetText("Parsing .pif ...", LogStatusType.Info);
			List<BaseRecord> baseRecords = _pifParser.Parse(input);
			_pifImporter.Import(baseRecords, storage, status);
		}
	}
}