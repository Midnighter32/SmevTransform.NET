### Description

A package that implements the transformation algorithm "urn://smev-gov-ru/xmldsig/transform"

### Purposes

When signing XML fragments of the ES in the XMLDSig format, it is mandatory to use the transformation urn://smev-gov-ru/xmldsig/transform.

### Transformation algorithm

1. XML declaration and processing instructions, if any, are removed;
2. If a text node contains only whitespace characters (character code less than or equal to '\u0020'), that text node is stripped;
3. After applying the rules from points 1 and 2, even if the element has no child nodes, the element cannot be represented as an empty element tag (http://www.w3.org/TR/2008/REC-xml-20081126/#sec-starttags, rule [44]), but must be converted to a start-tag + end-tag pair;
4. Remove namespace prefixes that are declared but not used at the current level;
5. Check that the current element's namespace is declared either up the tree or in the current element. If not declared, declare in current element;
6. Namespace prefix of elements and attributes should be replaced with auto-generated ones. The generated prefix consists of the literal "ns", and the serial number of the generated prefix within the XML fragment being processed, starting from one. When generating prefixes, their duplication should be eliminated; 
7. The attributes must be sorted alphabetically, first by namespace URI (if the attribute is in the qualified form), then by local name. Attributes in unqualified form after sorting come after attributes in qualified form;
8. The namespace prefix declarations must come before the attributes. Prefix declarations must be sorted in declaration order, as follows:
    + The element's namespace prefix is declared first, unless it was declared higher up the tree;
    + Next, attribute namespace prefixes are declared, if required. The order of these declarations corresponds to the order of the attributes, sorted alphabetically (see item 7 of the current list).

### Installation
```
dotnet add package SmevTransform.NET --version 1.0.0
```

### Usage
```csharp
using SmevTransform.NET;

Transform transform = new Transform();
transform.Process(srcXml);
```
