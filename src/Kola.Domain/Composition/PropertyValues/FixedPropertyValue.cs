﻿namespace Kola.Domain.Composition.PropertyValues
{
    using Kola.Domain.Instances.Config;

    public class FixedPropertyValue : IPropertyValue
    {
        public FixedPropertyValue(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }

        public string Resolve(IBuildSettings buildSettings)
        {
            var resolver = new ContextSourcedContentResolver(buildSettings?.ContextSets);
            return resolver.Resolve(this.Value);
        }

        public T Accept<T>(IPropertyValueVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public IPropertyValue Clone()
        {
            return new FixedPropertyValue(this.Value);
        }
    }
}