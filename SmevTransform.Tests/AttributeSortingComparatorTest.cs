using NUnit.Framework;
using SmevTransform.NET.Comparators;
using SmevTransform.NET.Data;

namespace SmevTransform.Tests
{
    public class AttributeSortingComparatorTest
    {
        [Test]
        public void CompareWithoutNsAttr1LessThan2Test()
        {
            var attr1 = new Attribute("name1", "value1");
            var attr2 = new Attribute("name2", "value2");

            var result = new AttributeSortingComparator().Compare(attr1, attr2);

            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void CompareWithoutNsAttr1EqualsAttr2()
        {
            var attr1 = new Attribute("name1", "value1");
            var attr2 = new Attribute("name1", "value2");

            var result = new AttributeSortingComparator().Compare(attr1, attr2);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CompareWithoutNsAttr1GreaterThanAttr2()
        {
            var attr1 = new Attribute("name2", "value1");
            var attr2 = new Attribute("name1", "value2");

            var result = new AttributeSortingComparator().Compare(attr1, attr2);

            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void CompareWithNsNs1LessThanNs2()
        {
            var attr1 = new Attribute("name1", "value1", "ns1", "prefix1");
            var attr2 = new Attribute("name2", "value2", "ns2", "prefix2");

            var result = new AttributeSortingComparator().Compare(attr1, attr2);

            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void CompareWithNsNs1GreaterThanNs2()
        {
            var attr1 = new Attribute("name2", "value2", "ns2", "prefix2");
            var attr2 = new Attribute("name1", "value1", "ns1", "prefix1");

            var result = new AttributeSortingComparator().Compare(attr1, attr2);

            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void CompareWithNs1WithoutNs2()
        {
            var attr1 = new Attribute("name1", "value1", "ns1", "prefix1");
            var attr2 = new Attribute("name2", "value2");

            var result = new AttributeSortingComparator().Compare(attr1, attr2);

            Assert.That(result, Is.LessThan(0));
        }

        [Test]
        public void CompareWithoutNs1WithNs2()
        {
            var attr1 = new Attribute("name1", "value1");
            var attr2 = new Attribute("name2", "value2", "ns2", "prefix2");

            var result = new AttributeSortingComparator().Compare(attr1, attr2);

            Assert.That(result, Is.GreaterThan(0));
        }
    }
}
