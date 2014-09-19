namespace _1Password2KeePass
{
	public class ComputerLicenseRecord : BaseRecord
	{
		public ComputerLicenseSecuteContents secureContents { get; set; }
	}

	public class ComputerLicenseSecuteContents
	{
		public string order_number { get; set; }
		public string publicsher_website { get; set; }
		public string download_link { get; set; }
		public string reg_name { get; set; }
		public string product_version { get; set; }
		public string reg_code { get; set; }
		public string publisher_name { get; set; }
		public string support_email { get; set; }

		// todo sections
	}
}