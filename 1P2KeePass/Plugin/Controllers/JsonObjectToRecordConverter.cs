using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _1Password2KeePass
{
	/// <summary>
	/// </summary>
	/// <remarks>
	/// http://stackoverflow.com/questions/8241392/deserializing-heterogenous-json-array-into-covariant-list-using-json-net
	/// http://james.newtonking.com/json/help/index.html?topic=html/CustomCreationConverter.htm
	/// </remarks>
	public class JsonObjectToRecordConverter : Newtonsoft.Json.Converters.CustomCreationConverter<BaseRecord>
	{
		/// <summary>
		/// Creates an object which will then be populated by the serializer.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// The created object.
		/// </returns>
		public override BaseRecord Create(Type objectType)
		{
			return new BaseRecord();
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param><param name="objectType">Type of the object.</param><param name="existingValue">The existing value of object being read.</param><param name="serializer">The calling serializer.</param>
		/// <returns>
		/// The object value.
		/// </returns>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jsonObject = JObject.Load(reader);

			BaseRecord target = Create(objectType, jsonObject);
			serializer.Populate(jsonObject.CreateReader(), target);												

			return target;
		}

		private BaseRecord Create(Type objectType, JObject jsonObject)
		{
			var jProperty = jsonObject.Property("typeName");
			string type = (string) jProperty;

			switch (type)
			{
				case "system.folder.Regular":
					return new FolderRecord();
				case "webforms.WebForm":
					return new WebFormRecord();
                case "wallet.financial.CreditCard":
                    return new CreditCardRecord();
                case "wallet.computer.License":
                    return new ComputerLicenseRecord();
                case "securenotes.SecureNote":
                    return new SecureNoteRecord();
                case "wallet.membership.Membership":
                    return new MembershipRecord();
                case "wallet.government.SsnUS":
                    return new SocialSecurityNumberRecord();
                case "wallet.onlineservices.GenericAccount":
                    return new GenericAccountRecord();
                case "wallet.financial.BankAccountUS":
                    return new BankAccountRecord();
                case "wallet.onlineservices.FTP":
                    return new FtpAccountRecord();
                case "wallet.computer.UnixServer":
                    return new UnixServerRecord();
                case "wallet.computer.Database":
                    return new DatabaseConnectionRecord();
                case "wallet.computer.Router":
                    return new RouterRecord();
                case "wallet.onlineservices.Email.v2":
                    return new EmailAccountRecord();
                case "passwords.Password":
                    return new GeneratedPasswordRecord();
            }
            //Trace.WriteLine("Unknown type: " + type);

			return new UnknownRecord();
		}
	}
}