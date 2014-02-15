﻿namespace Kola.Domain.Specifications
{
    using Kola.Domain.Composition;
    using Kola.Domain.Composition.ParameterValues;

    public class ParameterSpecification
    {
        public ParameterSpecification(string name, string type, string defaultValue = "")
        {
            this.Name = name;
            this.Type = type;
            this.DefaultValue = defaultValue;
        }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public string DefaultValue { get; private set; }

        public Parameter Create(IParameterValue parameterValue = null)
        {
            var value = parameterValue ?? (string.IsNullOrEmpty(this.DefaultValue)
                ? (IParameterValue)new UndefinedParameterValue()
                : new FixedParameterValue(this.DefaultValue));

            return new Parameter(this.Name, this.Type)
                {
                    Value = value
                };
        }
    }
}