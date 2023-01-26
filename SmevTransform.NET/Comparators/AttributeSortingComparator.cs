using SmevTransform.NET.Data;
using System.Collections.Generic;

namespace SmevTransform.NET.Comparators
{
    /// <summary>
    /// Represents a method for comparing two attributes
    /// </summary>
    public class AttributeSortingComparator : IComparer<Attribute>
    {
        /// <summary>
        /// Attribute sorting<br/>
        /// Attributes must be sorted alphabetically:<br/>
        /// first by namespace URI (if attribute is in qualified form),<br/>
        /// then - by local name.<br/>
        /// Attributes in unqualified form after sorting come after attributes in qualified form.
        /// </summary>
        /// <param name="attr1"></param>
        /// <param name="attr2"></param>
        public int Compare(Attribute attr1, Attribute attr2)
        {
            // both attributes are in unqualified form
            if (string.IsNullOrEmpty(attr1.getUri()) && string.IsNullOrEmpty(attr2.getUri()))
            {
                // compare attribute names
                return attr1.getName().CompareTo(attr2.getName());
            }

            // both attributes are in unqualified form
            if (!string.IsNullOrEmpty(attr1.getUri()) && !string.IsNullOrEmpty(attr2.getUri()))
            {
                // compare namespace
                var nsComparsionResult = attr1.getUri().CompareTo(attr2.getUri());
                if (nsComparsionResult != 0)
                    return nsComparsionResult;
                else
                    return attr1.getName().CompareTo(attr2.getName());
            }

            return string.IsNullOrEmpty(attr1.getUri()) ? 1 : -1;
        }
    }
}
