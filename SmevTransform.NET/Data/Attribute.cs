namespace SmevTransform.NET.Data
{
    /// <summary>
    /// Represents information about an XML attribute
    /// </summary>
    public class Attribute
    {
        private string name;

        private string value;

        private string? uri;

        private string? prefix;

        public Attribute(string name, string value, string? uri = null, string? prefix = null)
        {
            this.name = name;
            this.value = value;
            this.uri = uri;
            this.prefix = prefix;
        }

        /// <summary>
        /// Get attribute name
        /// </summary>
        /// <returns></returns>
        public string getName() => this.name;

        /// <summary>
        /// Get attribute value
        /// </summary>
        /// <returns></returns>
        public string getValue() => this.value;

        /// <summary>
        /// Get namespace address
        /// </summary>
        /// <returns></returns>
        public string? getUri() => this.uri;

        /// <summary>
        /// Get namespace prefix
        /// </summary>
        /// <returns></returns>
        public string? getPrefix() => this.prefix;
    }
}
