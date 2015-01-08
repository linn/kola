﻿namespace Kola.Configuration.Plugins
{
    using System.Collections.Generic;

    using Kola.Configuration.Fluent;
    using Kola.Domain.Composition;
    using Kola.Domain.Specifications;

    public abstract class PluginConfiguration
    {
        private readonly List<IPluginComponentSpecification<IComponentWithProperties>> componentSpecifications = new List<IPluginComponentSpecification<IComponentWithProperties>>();
        private readonly List<PropertyTypeSpecification> propertySpecifications = new List<PropertyTypeSpecification>();

        public string ViewLocation { get; set; }

        internal IEnumerable<IPluginComponentSpecification<IComponentWithProperties>> ComponentTypeSpecifications
        {
            get { return this.componentSpecifications; }
        }

        internal IEnumerable<PropertyTypeSpecification> PropertyTypeSpecifications
        {
            get { return this.propertySpecifications; }
        }

        protected PluginConfigurer Configure
        {
            get { return new PluginConfigurer(this); }
        }

        internal void Add(IPluginComponentSpecification<IComponentWithProperties> componentSpecification)
        {
            this.componentSpecifications.Add(componentSpecification);
        }

        internal void Add(PropertyTypeSpecification propertyTypeSpecification)
        {
            this.propertySpecifications.Add(propertyTypeSpecification);
        }
    }
}
