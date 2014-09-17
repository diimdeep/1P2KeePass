namespace _1Password2KeePass
{
	public class SecureNoteRecord : BaseRecord
	{
		public SecureNoteSecuteContents secureContents { get; set; }
	}

	public class SecureNoteSecuteContents
	{
		public string notesPlain { get; set; }
	}
}