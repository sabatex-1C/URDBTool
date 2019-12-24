using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NTICS
{
    [DesignTimeVisible(false)]
    public partial class Frame : UserControl
    {
        public Frame()
        {
            InitializeComponent();
        }
        public void SaveProperty(string PropertyName, string Property)
        {
            // Устанавливаемся на узел соответствующий даному обэкту
            XmlNode node = config.XML.DocumentElement.SelectSingleNode("descendant::" + this.Name);
            if (node == null)
            {
                node = config.XML.DocumentElement.AppendChild(config.XML.CreateNode(XmlNodeType.Element, this.Name, ""));
            }

            // ищем нужний атрибут
            XmlNode atr = node.Attributes.GetNamedItem(PropertyName);
            if (atr == null)
            {
                atr = config.XML.CreateAttribute(PropertyName);
                node.Attributes.Append((XmlAttribute)atr);
            }

            atr.Value = Property;
        }
        public string LoadProperty(string PropertyName, string Default)
        {
            // Устанавливаемся на узел соответствующий даному обэкту
            XmlNode node = config.XML.DocumentElement.SelectSingleNode("descendant::" + this.Name);
            if (node == null)
            {
                node = config.XML.DocumentElement.AppendChild(config.XML.CreateNode(XmlNodeType.Element, this.Name, ""));
            }

            // ищем нужний атрибут
            XmlNode atr = node.Attributes.GetNamedItem(PropertyName);
            if (atr == null)
            {
                atr = config.XML.CreateAttribute(PropertyName);
                node.Attributes.Append((XmlAttribute)atr);
                atr.Value = Default;
            }

            return atr.Value;
        }

    }
}
