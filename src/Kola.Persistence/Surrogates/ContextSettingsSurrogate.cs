﻿namespace Kola.Persistence.Surrogates
{
    using System.Xml.Serialization;

    [XmlRoot(Namespace = "http://www.kolacms.com/2013/kola", ElementName = "contextSettings")]
    public class ContextSettingsSurrogate
    {
        [XmlArray("contextItems")]
        public ContextItemSurrogate[] ContextItems { get; set; }
    }
}