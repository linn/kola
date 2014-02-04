﻿namespace Kola.Nancy
{
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Configuration;
    using Kola.Domain;
    using Kola.Domain.Specifications;
    using Kola.Domain.Templates;
    using Kola.Persistence;

    public class ComponentLibrary : IComponentSpecificationLibrary
    {
        private readonly IKolaConfigurationRegistry registry;

        private readonly IWidgetSpecificationRepository widgetRepository;

        public ComponentLibrary(IKolaConfigurationRegistry registry, IWidgetSpecificationRepository widgetRepository)
        {
            this.registry = registry;
            this.widgetRepository = widgetRepository;
        }

        public IEnumerable<IComponentSpecification<IComponentTemplate>> FindAll()
        {
            IEnumerable<IComponentSpecification<IComponentTemplate>> pluggedInComponents = this.registry.KolaConfiguration.Plugins
                .SelectMany(plugin => plugin.Components);

            var widgets = this.widgetRepository.FindAll();

            return pluggedInComponents.Concat(widgets);
        }

        public IComponentSpecification<IComponentTemplate> Lookup(string componentName)
        {
            var component = this.registry.KolaConfiguration.Plugins
                .SelectMany(plugin => plugin.Components)
                .FirstOrDefault(c => c.Name == componentName);

            return component != null
                ? (IComponentSpecification<IComponentTemplate>)component
                : this.widgetRepository.Find(componentName);
        }
    }
}