using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace NTICS
{
    public class config
    {
        public static TraceConsoleListner ConsoleListner = new TraceConsoleListner();
        
        static XmlDocument xml;

        public static string GetConfigFileName()
        {
            return Application.StartupPath + "\\Config.xml";
        }

        public static TextWriterTraceListener LogFile = new TextWriterTraceListener(Application.StartupPath + "\\pub-zik.log");


        public static void Initialize()
        {
            if (!File.Exists(GetConfigFileName()))
            {
                XmlTextWriter tw = new XmlTextWriter(GetConfigFileName(), null);
                tw.WriteStartDocument();
                tw.WriteStartElement("CONFIG");
                tw.WriteEndElement();
                tw.Close();

            }
            xml = new XmlDocument();
            xml.Load(GetConfigFileName());
            Trace.Listeners.Add(LogFile);
            Trace.Listeners.Add(ConsoleListner);

        }

        public static XmlDocument XML
        {
            get { return xml; }
        }

        public static void Close()
        {
            xml.Save(GetConfigFileName());
        }

        public static void SaveProperty(String Sender, string PropertyName, string Property)
        {
            // Устанавливаемся на узел соответствующий даному обэкту
            XmlNode node = config.XML.DocumentElement.SelectSingleNode("descendant::" + Sender);
            if (node == null)
            {
                node = config.XML.DocumentElement.AppendChild(config.XML.CreateNode(XmlNodeType.Element, Sender, ""));
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
        public static string LoadProperty(string Sender, string PropertyName, string Default)
        {
            // Устанавливаемся на узел соответствующий даному обэкту
            XmlNode node = config.XML.DocumentElement.SelectSingleNode("descendant::" + Sender);
            if (node == null)
            {
                node = config.XML.DocumentElement.AppendChild(config.XML.CreateNode(XmlNodeType.Element, Sender, ""));
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
