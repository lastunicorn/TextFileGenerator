﻿using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.TestingTools
{
    public class XmlAsserter
    {
        private readonly XPathNavigator xPathNavigator;
        private readonly XmlNamespaceManager namespaceManager;

        public XmlAsserter(Stream stream)
        {
            Log(stream);

            XPathDocument xPathDocument = new XPathDocument(stream);
            xPathNavigator = xPathDocument.CreateNavigator();

            namespaceManager = new XmlNamespaceManager(xPathNavigator.NameTable);
        }

        private static void Log(Stream stream)
        {
            Trace.WriteLine("========================================================================================");
            Trace.WriteLine("Start asserting on xml");
            Trace.WriteLine("========================================================================================");

            long initialPosition = stream.Position;

            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();

            stream.Position = initialPosition;

            Trace.WriteLine(string.Empty);
            Trace.WriteLine(s);
            Trace.WriteLine(string.Empty);
            Trace.WriteLine("========================================================================================");
            Trace.WriteLine(string.Empty);
        }

        public void AddNamespace(string prefix, string value)
        {
            namespaceManager.AddNamespace(prefix, value);
        }

        public void AssertNodeCount(string xpath, int expectedCount)
        {
            XPathNodeIterator nodeIterator = xPathNavigator.Select(xpath, namespaceManager);
            
            string message = string.Format("Node '{0}' was not found as expected.", xpath);
            Assert.That(nodeIterator.Count, Is.EqualTo(expectedCount), message);
        }

        public void AssertText(string xpath, string expected)
        {
            XPathNodeIterator nodeIterator = xPathNavigator.Select(xpath, namespaceManager);

            if (nodeIterator.Count == 0)
                Assert.Fail("No node was matched for xpath: {0}.", xpath);

            while (nodeIterator.MoveNext())
            {
                string message = string.Format("Node '{0}' does not contain expected text.", xpath);
                Assert.That(nodeIterator.Current.Value, Is.EqualTo(expected), message);
            }
        }
    }
}