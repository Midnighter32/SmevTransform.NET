using NUnit.Framework;
using SmevTransform.NET;

namespace SmevTransform.Tests
{
    public class TransformTest
    {
        private string srcXml;
        private string expectedXml;

        [SetUp]
        public void Setup()
        {
            srcXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + 
                     "<?xml-stylesheet type=\"text/xsl\" href=\"style.xsl\"?>" + 
                     "<elementOne xmlns=\"http://test/1\" xmlns:qwe=\"http://test/2\" xmlns:asd=\"http://test/3\">" + 
                         "<qwe:elementTwo>" +
                             "<asd:elementThree " +
                                 "xmlns:wer=\"http://test/a\" " +
                                 "xmlns:zxc=\"http://test/0\" " +
                                 "wer:attZ=\"zzz\" " +
                                 "attB=\"bbb\" " +
                                 "attA=\"aaa\" " +
                                 "zxc:attC=\"ccc\" " +
                                 "asd:attD=\"ddd\" " +
                                 "asd:attE=\"eee\" " +
                                 "qwe:attF=\"fff\" " +
                             "/>" +
                         "</qwe:elementTwo>" +
                         "<qwe:elementFour>0</qwe:elementFour>" +
                         "<qwe:elementFive/>" +
                         "<qwe:elementSix>   </qwe:elementSix>" +
                     "</elementOne>";

            expectedXml = "<ns1:elementOne xmlns:ns1=\"http://test/1\">" +
                                "<ns2:elementTwo xmlns:ns2=\"http://test/2\">" +
                                    "<ns3:elementThree xmlns:ns3=\"http://test/3\" xmlns:ns4=\"http://test/0\" xmlns:ns5=\"http://test/a\" ns4:attC=\"ccc\" ns2:attF=\"fff\" ns3:attD=\"ddd\" ns3:attE=\"eee\" ns5:attZ=\"zzz\" attA=\"aaa\" attB=\"bbb\"></ns3:elementThree>" +
                                "</ns2:elementTwo>" +
                                "<ns2:elementFour xmlns:ns2=\"http://test/2\">0</ns2:elementFour>" +
                                "<ns2:elementFive xmlns:ns2=\"http://test/2\"></ns2:elementFive>" +
                                "<ns2:elementSix xmlns:ns2=\"http://test/2\"></ns2:elementSix>" +
                            "</ns1:elementOne>";
        }

        [Test]
        public void ProcessTest()
        {
            Transform transform = new Transform();

            Assert.IsNotNull(transform);

            Assert.That(transform.Process(srcXml), Is.EqualTo(expectedXml));

            Assert.Pass();
        }
    }
}