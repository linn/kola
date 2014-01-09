﻿/*
 * The role of the bootstrapper:
 * 1. One-time scan of referenced assemblies to find plug-ins; for each plug-in:
 *    a. Add the view location to a list to be used by the host to resolve views
 *    b. Add the assembly to a list to be used by the host
 *    c. Add a handler for each component configuration: 
 * 2. Create a common instance of the kola engine and put it somewhere reusable
 */
namespace Kola.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Configuration.Plugins;
    using Kola.Rendering;

    public abstract class KolaConfigurationBuilder
    {
        public KolaConfiguration Build()
        {
            var plugins = this.FindPlugins();

            var handlerMappings = plugins
                .SelectMany(c => c.Components)
                .ToDictionary(c => c.Name, c => c.HandlerType);

            return new KolaConfiguration(new KolaEngine(this.BuildProcessor(handlerMappings)), plugins);
        }

        protected abstract IEnumerable<PluginConfiguration> FindPlugins();

        protected abstract IProcessor BuildProcessor(Dictionary<string, Type> handlerMappings);
    }
}
