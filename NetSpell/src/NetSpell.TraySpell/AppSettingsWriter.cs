// Copyright (C) 2003  Paul Welter
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

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
