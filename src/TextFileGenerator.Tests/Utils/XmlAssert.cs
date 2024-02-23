// TextFileGenerator
// Copyright (C) 2009-2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Utils;

public class XmlAssert
{
    private readonly XPathNavigator xPathNavigator;
    private readonly XmlNamespaceManager namespaceManager;

    public XmlAssert(Stream stream)
    {
        Log(stream);

        XPathDocument xPathDocument = new(stream);
        xPathNavigator = xPathDocument.CreateNavigator();

        namespaceManager = new XmlNamespaceManager(xPathNavigator.NameTable);
    }

    private static void Log(Stream stream)
    {
        Trace.WriteLine("========================================================================================");
        Trace.WriteLine("Start asserting on xml");
        Trace.WriteLine("========================================================================================");

        long initialPosition = stream.Position;

        StreamReader sr = new(stream);
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

        Assert.That(nodeIterator.Count, Is.EqualTo(expectedCount), $"Node '{xpath}' was not found as expected.");
    }

    public void AssertText(string xpath, string expected)
    {
        XPathNodeIterator nodeIterator = xPathNavigator.Select(xpath, namespaceManager);

        if (nodeIterator.Count == 0)
            Assert.Fail($"No node was matched for xpath: {xpath}.");

        while (nodeIterator.MoveNext())
        {
            Assert.That(nodeIterator.Current.Value, Is.EqualTo(expected), $"Node '{xpath}' does not contain expected text.");
        }
    }
}