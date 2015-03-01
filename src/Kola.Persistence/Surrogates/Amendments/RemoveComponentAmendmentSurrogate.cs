﻿namespace Kola.Persistence.Surrogates.Amendments
{
    using System.Xml.Serialization;

    [XmlType(Namespace = "http://www.kolacms.com/2013/kola", TypeName = "removeComponent")]
    public class RemoveComponentAmendmentSurrogate : AmendmentSurrogate
    {
        [XmlElement("componentPath")]
        public string ComponentPath { get; set; }

        public override T Accept<T>(IAmendmentSurrogateVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}