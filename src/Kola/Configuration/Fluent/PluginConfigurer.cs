﻿
namespace Kola.Configuration.Fluent
{
    using Kola.Configuration.Plugins;
    using Kola.Domain.Specifications;

    public class PluginConfigurer
    {
        private readonly PluginConfiguration configuration;

        internal PluginConfigurer(PluginConfiguration pluginConfiguration)
        {
            this.configuration = pluginConfiguration;
        }

        public void ViewLocation(string viewLocation)
        {
            this.configuration.ViewLocation = viewLocation;
        }

        public ComponentConfigurer Container(string componentName)
        {
            var specification = new ContainerSpecification(componentName);

            this.configuration.Add(specification);

            return new ComponentConfigurer(specification);
        }

        public ComponentConfigurer Atom(string componentName)
        {
            var specification = new AtomSpecification(componentName);

            this.configuration.Add(specification);

            return new ComponentConfigurer(specification);
        }

        public PropertyTypeConfigurer PropertyType(string propertyName)
        {
            var specification = new PropertyTypeSpecification(propertyName);

            this.configuration.Add(specification);

            return new PropertyTypeConfigurer(specification);
        }
    }
}