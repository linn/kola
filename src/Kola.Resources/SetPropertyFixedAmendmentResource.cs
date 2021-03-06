﻿namespace Kola.Resources
{
    public class SetPropertyFixedAmendmentResource : AmendmentResource
    {
        public string ComponentPath { get; set; }

        public string PropertyName { get; set; }

        public string Value { get; set; }

        public override string Type => "Set Property Fixed";

        public override T Accept<T>(IAmendmentResourceVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}