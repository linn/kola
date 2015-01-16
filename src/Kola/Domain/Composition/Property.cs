﻿namespace Kola.Domain.Composition
{
    using Kola.Domain.Composition.PropertyValues;
    using Kola.Domain.Instances;
    using Kola.Domain.Instances.Building;

    public class Property
    {
        private IPropertyValue value;

        public Property(string name, string type, IPropertyValue value)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
        }

        public string Name { get; private set; }

        public string Type { get; private set; }


        public IPropertyValue Value
        {
            get
            {
                return this.value;
            }
            
            set
            {
                if (value == null)
                {
                    throw new KolaException("Property must have a value");
                }

                this.value = value;
            }
        }

        public PropertyInstance Build(IBuildContext buildContext)
        {
            return new PropertyInstance(this.Name, this.Value.Resolve(buildContext));
        }
    }
}