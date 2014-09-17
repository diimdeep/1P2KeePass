namespace _1Password2KeePass
{
	public class CreditCardRecord : BaseRecord
	{
		public CreditCardSecuteContents secureContents { get; set; }		
	}

	public class CreditCardSecuteContents
	{
		public string expiry_yy { get; set; }
		public string expiry_mm { get; set; }
		public string cardholder { get; set; }
		public string website { get; set; }
		public string bank { get; set; }
		public string type { get; set; }
		public string ccnum { get; set; }

		// todo sections
	}
}