// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Xml;
using System.Reflection;

namespace NetSpell.TraySpell
{
	/// <summary>
	/// Summary description for AppSettingsWriter.
	/// </summary>
	public class AppSettingsWriter
	{
		private string _configFileName;
		private XmlDocument _configDocument = new XmlDocument();

		public AppSettingsWriter()
		{
			Assembly asmy = Assembly.GetEntryAssembly();
            _configFileName = asmy.Location + ".config";
            _configDocument.Load(_configFileName);
		}

		public void SetValue(string key, string value)
		{
			string xpath = string.Format("/configuration/appSettings/add[@key=\"{0}\"]", key);
			XmlNode node = _configDocument.SelectSingleNode(xpath);

			if(node == null)
			{
				XmlElement element = _configDocument.CreateElement("add");
				element.SetAttribute("key", key);
				element.SetAttribute("value", value);

                xpath = "/configuration/appSettings";
                XmlNode root = _configDocument.DocumentElement.SelectSingleNode(xpath);
				
				root.AppendChild((XmlNode)element);
			}
			else
			{
				node.Attributes.GetNamedItem("value").Value = value;
			}

			_configDocument.Save(_configFileName);
		}
	}
}
