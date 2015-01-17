﻿namespace Kola.Domain.Composition
{
    using System.Collections.Generic;

    using Kola.Domain.Extensions;
    using Kola.Domain.Instances;
    using Kola.Domain.Instances.Building;
    using Kola.Domain.Specifications;

    public abstract class ComponentWithProperties : IComponentWithProperties
    {
        private readonly List<Property> properties = new List<Property>();

        protected ComponentWithProperties(string name, IEnumerable<Property> properties)
        {
            this.Name = name;

            if (properties != null)
            {
                this.properties.AddRange(properties);
            }
        }

        public string Name { get; private set; }

        public IEnumerable<Property> Properties
        {
            get { return this.properties; }
        }

        public Property FindOrCreateProperty(PropertySpecification specification)
        {
            var property = this.properties.Find(specification.Name);

            if (property == null)
            {
                property = specification.Create();
                this.properties.Add(property);
            }

            return property;
        }

        public abstract void Accept(IComponentVisitor visitor);

        public abstract T Accept<T>(IComponentVisitor<T> visitor);

        public abstract T Accept<T, TContext>(IComponentVisitor<T, TContext> visitor, TContext context);

        public abstract ComponentInstance Build(IEnumerable<int> path, IBuildContext buildContext);
    }
}