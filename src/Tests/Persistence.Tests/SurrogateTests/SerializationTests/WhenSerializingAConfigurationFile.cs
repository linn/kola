﻿namespace Persistence.Tests.SurrogateTests.SerializationTests
{
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    using FluentAssertions;

    using Kola.Persistence.Surrogates;
    using Kola.Persistence.Surrogates.ContextItems;

    using NUnit.Framework;

    public class WhenSerializingAConfigurationFile
    {
        private string result;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationSurrogate
                             {
                                 ContextItems =
                                     new ContextItemSurrogate[]
                                         {
                                             new FixedContextItemSurrogate
                                                 {
                                                     Name =
                                                         "Fixed 1 Name",
                                                     Value =
                                                         "Fixed 1 Value"
                                                 },
                                             new RandomContextItemSurrogate
                                                 {
                                                 ContextItemGroups = new []
                                                                         {
                                                                             new ContextItemGroup
                                                                                 {
                                                                                     ContextItems = new []
                                                                                                        {
                                                                                                            new FixedContextItemSurrogate
                                                                                                                 {
                                                                                                                     Name =
                                                                                                                         "Fixed 2 Name",
                                                                                                                     Value =
                                                                                                                         "Fixed 2 Value"
                                                                                                                 },
                                                                                                        }
                                                                                 },
                                                                             new ContextItemGroup()
                                                                         }
                                                 }
                                         }
                             };

            var serializer = new XmlSerializer(typeof(ConfigurationSurrogate));
            var sb = new StringBuilder();
            serializer.Serialize(new StringWriter(sb), config);
            this.result = sb.ToString();
        }

        [Test]
        public void TheExpectedOutputShouldHappen2()
        {
            this.result.Should().BeEquivalentTo(
@"<?xml version=""1.0"" encoding=""utf-16""?>
<configuration xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://www.kolacms.com/2013/kola"">
  <contextItems>
    <contextItem name=""Fixed 1 Name""><![CDATA[Fixed 1 Value]]></contextItem>
    <randomContext>
      <contextItems>
        <contextItem name=""Fixed 2 Name""><![CDATA[Fixed 2 Value]]></contextItem>
      </contextItems>
      <contextItems />
    </randomContext>
  </contextItems>
  <cacheControl><![CDATA[]]></cacheControl>
</configuration>");
        }
    }
}
