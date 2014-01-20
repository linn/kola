﻿namespace Kola.Domain.Templates.ParameterValues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MultilingualParameterValue : IParameterValue
    {
        public MultilingualParameterValue(IEnumerable<MultilingualVariant> variants)
        {
            this.Variants = variants;
        }

        public IEnumerable<MultilingualVariant> Variants { get; private set; }

        public string Resolve(IEnumerable<Context> contexts)
        {
            var candidateLanguages = contexts.Where(c => !string.IsNullOrEmpty(c.LanguageCode)).Select(c => c.LanguageCode);

            foreach (var candidateLanguage in candidateLanguages)
            {
                var language = candidateLanguage;
                var variant =
                    this.Variants.Where(
                        v => v.LanguageCode.Equals(language, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (variant != null)
                {
                    return variant.Value;
                }
            }

            return string.Empty;
        }
    }
}