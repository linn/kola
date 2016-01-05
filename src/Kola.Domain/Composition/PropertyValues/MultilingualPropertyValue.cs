﻿namespace Kola.Domain.Composition.PropertyValues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Domain.Extensions;
    using Kola.Domain.Instances.Config;

    public class MultilingualPropertyValue : IPropertyValue
    {
        public MultilingualPropertyValue(IEnumerable<MultilingualVariant> variants)
        {
            this.Variants = variants;
        }

        public IEnumerable<MultilingualVariant> Variants { get; private set; }

        public string Resolve(IBuildSettings buildSettings)
        {
            var candidateLanguages = buildSettings.ContextSets.Where(c => !string.IsNullOrEmpty(c.LanguageCode)).Select(c => c.LanguageCode);

            foreach (var candidateLanguage in candidateLanguages)
            {
                var language = candidateLanguage;
                var variant = this.Variants.FirstOrDefault(v => v.LanguageCode.Equals(language, StringComparison.OrdinalIgnoreCase));

                if (variant != null)
                {
                    return variant.Value;
                }
            }

            return string.Empty;
        }

        public T Accept<T>(IPropertyValueVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public IPropertyValue Clone()
        {
            return new MultilingualPropertyValue(this.Variants.Clone());
        }
    }
}