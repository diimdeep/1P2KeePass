using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace _1Password2KeePass
{
	public class PIFParser
	{
		private JsonObjectToRecordConverter _converter = new JsonObjectToRecordConverter();

		public List<BaseRecord> Parse(Stream input)
		{
			Trace.WriteLine(String.Format("Info. Begin parsing."));

			TextReader tr = new StreamReader(input);
			string readLine = tr.ReadLine();			
			uint parsedCount = 0;			

			List<BaseRecord> brecords = new List<BaseRecord>();

			while (readLine != null)
			{
				if (readLine.StartsWith("{"))
				{
					parsedCount++;
					Trace.WriteLine(String.Format("{0} ", parsedCount));

					var deserializeObject = JsonConvert.DeserializeObject<BaseRecord>(readLine, _converter);
					brecords.Add(deserializeObject);
				}
				else if (readLine.StartsWith("***"))
				{
				}
				else
				{
					Trace.WriteLine(String.Format("\n Error. Wrong format: {0}", readLine));
				}

				readLine = tr.ReadLine();
			}

			Trace.WriteLine(String.Format("\n Info. Parsing complete. Parsed: {0}", parsedCount));

			return brecords;
		}
	}
}