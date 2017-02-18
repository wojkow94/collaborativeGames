using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ProjektGrupowy.Models.Xml
{
    abstract public class XmlEntity<T>
    {
        abstract protected void BeforeSerialization(T o);
        abstract protected void AfterDeserialization(T o);

        protected Type[] extraTypes;

        public XmlEntity(Type[] extraTypes)
        {
            this.extraTypes = extraTypes;
        }

        public string Serialize(T o)
        {
            BeforeSerialization(o);

            StringBuilder sb = new StringBuilder();             
            using (XmlWriter writer = XmlWriter.Create(new StringWriter(sb)))
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                serializer.Serialize(writer, o);
            }

            return sb.ToString();
        }
        public T Deserialize(string xml)
        {
            T result = default(T);
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                if (extraTypes != null)
                {
                    foreach (Type t in extraTypes)
                    {
                        XmlSerializer serializer = new XmlSerializer(t);
                        if (serializer.CanDeserialize(reader))
                            result = (T)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    result = (T)serializer.Deserialize(reader);
                }
            }
            AfterDeserialization(result);

            return result;
        }
    }
}