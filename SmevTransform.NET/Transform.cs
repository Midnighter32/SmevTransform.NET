using SmevTransform.NET.Comparators;
using SmevTransform.NET.Data;
using SmevTransform.NET.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace SmevTransform.NET
{
    /// <summary>
    /// Represents an algorithm for transforming an XML document
    /// </summary>
    public class Transform
    {
        private const string Algorithm_Urn = "urn://smev-gov-ru/xmldsig/transform";

        private readonly static Encoding XML_Encoding = Encoding.UTF8;

        private Stack<List<XmlNamespace>> prefixStack = null;

        /// <summary>
        /// Transforms XML using the algorithm - urn://smev-gov-ru/xmldsig/transform
        /// </summary>
        /// <param name="xml">Source string in XML format</param>
        /// <returns>Transformed string in XML format</returns>
        public string Process(string xml)
        {
            prefixStack = new Stack<List<XmlNamespace>>();
            var prefixCnt = 1;

            XmlReader reader = null;
            XmlWriter writer = null;

            try
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    ConformanceLevel = ConformanceLevel.Fragment
                };

                using var ms = new MemoryStream(XML_Encoding.GetBytes(xml));

                StringBuilder transformed = new StringBuilder();

                reader = XmlReader.Create(ms);

                writer = XmlWriter.Create(transformed, settings);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        if (reader.Value.Trim() != string.Empty)
                            writer.WriteRaw(reader.Value);
                        continue;
                    }
                    else if (reader.NodeType == XmlNodeType.Element)
                    {
                        var currentPrefixStack = new List<XmlNamespace>();

                        prefixStack.Push(currentPrefixStack);

                        var nsUri = reader.NamespaceURI ?? string.Empty;
                        var prefix = FindPrefix(nsUri);

                        if (prefix == null)
                        {
                            prefix = "ns" + prefixCnt++;
                            currentPrefixStack.Add(new XmlNamespace(nsUri, prefix));
                        }

                        writer.WriteStartElement(prefix, reader.LocalName, nsUri);

                        List<Attribute> srcAttributes = new List<Attribute>();
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);

                            if (reader.Value == nsUri || reader.Name.Contains("xmlns"))
                                continue;

                            var dstAttr = new Attribute(
                                reader.LocalName,
                                reader.Value,
                                reader.NamespaceURI,
                                reader.Prefix
                            );
                            srcAttributes.Add(dstAttr);
                        }
                        reader.MoveToElement();

                        srcAttributes.Sort(new AttributeSortingComparator());

                        List<Attribute> dstAttributes = new List<Attribute>();
                        foreach (var srcAttribute in srcAttributes)
                        {
                            if (srcAttribute.getUri() != string.Empty)
                            {
                                var attrPrefix = FindPrefix(srcAttribute.getUri());
                                if (attrPrefix == null)
                                {
                                    attrPrefix = "ns" + prefixCnt++;
                                    currentPrefixStack.Add(new XmlNamespace(srcAttribute.getUri(), attrPrefix));
                                }
                                dstAttributes.Add(new Attribute(
                                    srcAttribute.getName(),
                                    srcAttribute.getValue(),
                                    srcAttribute.getUri(),
                                    attrPrefix
                                ));
                            }
                            else
                            {
                                dstAttributes.Add(new Attribute(
                                    srcAttribute.getName(),
                                    srcAttribute.getValue()
                                ));
                            }
                        }

                        foreach (var @namespace in currentPrefixStack)
                        {
                            writer.WriteAttributeString("xmlns", @namespace.getPrefix(), null, @namespace.getUri());
                        }

                        foreach (var dstAttribute in dstAttributes)
                        {
                            if (dstAttribute.getUri() != null)
                            {
                                writer.WriteAttributeString(
                                    dstAttribute.getPrefix(),
                                    dstAttribute.getName(),
                                    null,
                                    dstAttribute.getValue());
                            }
                            else
                            {
                                writer.WriteAttributeString(dstAttribute.getName(), dstAttribute.getValue());
                            }
                        }

                        if (reader.IsEmptyElement)
                        {
                            writer.WriteFullEndElement();
                        }

                        continue;
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        writer.WriteRaw("");

                        writer.WriteFullEndElement();

                        prefixStack.Pop();

                        continue;
                    }
                }

                writer.Flush();
                return transformed.ToString();
            } 
            catch (System.Exception ex)
            {
                throw new TransformationException(
                    "Can not perform transformation " + Algorithm_Urn,
                    ex
                );
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (writer != null)
                    writer.Flush();
            }
        }

        /// <summary>
        /// Finds the created namespace given the given Uri
        /// </summary>
        /// <param name="uri">Namespace address</param>
        /// <returns>Prefix corresponding to the passed address</returns>
        private string FindPrefix(string uri)
        {
            foreach (var currentprefixStack in prefixStack)
            {
                foreach (var @namespace in currentprefixStack)
                {
                    if (uri == @namespace.getUri())
                    {
                        return @namespace.getPrefix();
                    }
                }
            }

            return null;
        }
    }
}