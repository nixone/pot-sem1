using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GameLib
{
    public class SerializableHashSet<T> : HashSet<T>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(); // SET

            reader.ReadStartElement(); // items
            int count = reader.ReadElementContentAsInt();

            for (int i = 0; i < count; i++)
            {
                reader.ReadStartElement(); // item 
                String typeName = reader.ReadElementContentAsString();

                reader.ReadStartElement();
                XmlSerializer serializer = new XmlSerializer(Type.GetType(typeName));
                T value = (T)serializer.Deserialize(reader);
                Add(value);
                reader.ReadEndElement();
                reader.ReadEndElement();
            }

            reader.ReadEndElement();
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("items");

            writer.WriteStartElement("count");
            writer.WriteString("" + this.Count());
            writer.WriteEndElement();

            foreach (T item in this)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("type");
                writer.WriteString(item.GetType().AssemblyQualifiedName);
                writer.WriteEndElement();

                writer.WriteStartElement("content");
                new XmlSerializer(item.GetType()).Serialize(writer, item);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }
    }
}
