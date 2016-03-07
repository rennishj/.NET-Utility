using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
namespace RJ.Utils
{   

    /// <summary>
    /// This is the Utility class for Xml Serialization
    /// </summary>
    public class XmlUtils
    {
        public XmlUtils()
        {
            this.ValidationErrors = new List<string>();
        }
        public List<string> ValidationErrors { get; set; }
        public string XmlStringSerialize<T>(T obj) where T : class
        {
            XmlSerializer ser = new XmlSerializer(obj.GetType());

            using (StringWriter textWriter = new Utf8StringWriter())
            {
                ser.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }
        /// <summary>
        /// Validating an xml from xml string instead of a file.Please refere to http://stackoverflow.com/questions/4601139/how-to-read-a-xml-string-into-xmltextreader-type
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public List<string> IsValidXml(string xml)
        {

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, @"C:\DotNetApplications\TestXmlFiles\ShippingStation.xsd");
            settings.ValidationType = ValidationType.Schema;
            settings.IgnoreWhitespace = true;
            settings.ValidationEventHandler += XmlValidationErrorHandler;
            using (XmlReader rdr = XmlReader.Create(new XmlTextReader(new StringReader(xml)), settings))
            {
                while (rdr.Read())
                {

                }
            }

            return this.ValidationErrors;
            //testing a check in

        }

        public void XmlValidationErrorHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {

            if (e.Severity == System.Xml.Schema.XmlSeverityType.Error)
            {
                this.ValidationErrors.Add(e.Message);
            }
        }
    }
    /// <summary>
    /// This is created because the default encoding is UTF-16 and ur doc says it should be UTF-8
    /// </summary>
    public class Utf8StringWriter : StringWriter
    {
        public override System.Text.Encoding Encoding
        {
            get
            {
                return System.Text.Encoding.UTF8;
            }
        }
    }

}
