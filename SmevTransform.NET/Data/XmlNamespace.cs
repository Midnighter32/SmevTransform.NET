using System;

namespace SmevTransform.NET.Data
{
    /// <summary>
    /// Represents information about the XML namespace
    /// </summary>
    internal class XmlNamespace
    {
        private string uri;
        private string? prefix;

        public XmlNamespace(string uri, string? prefix = null)
        {
            if (uri == null)
                throw new ArgumentException("Passed empty URI");

            this.prefix = prefix;

            this.uri = uri;
        }

        /// <summary>
        /// Get namespace prefix
        /// </summary>
        /// <returns></returns>
        public string? getPrefix()
        {
            return this.prefix;
        }

        /// <summary>
        /// Get namespace address
        /// </summary>
        /// <returns></returns>
        public string getUri()
        {
            return this.uri;
        }
    }
}
