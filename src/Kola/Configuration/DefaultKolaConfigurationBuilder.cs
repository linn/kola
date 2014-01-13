﻿namespace Kola.Configuration
{
    using System;
    using System.Collections.Generic;

    using Kola.Configuration.Plugins;
    using Kola.Rendering;

    public class DefaultKolaConfigurationBuilder : KolaConfigurationBuilder
    {
        private readonly IPluginFinder pluginFinder;

        private readonly IObjectFactory objectFactory;

        public DefaultKolaConfigurationBuilder(IPluginFinder pluginFinder, IObjectFactory objectFactory)
        {
            this.pluginFinder = pluginFinder;
            this.objectFactory = objectFactory;
        }

        protected override IEnumerable<PluginConfiguration> FindPlugins()
        {
            return this.pluginFinder.FindPlugins();
        }

        protected override IProcessor BuildProcessor(Dictionary<string, Type> handlerMappings, EngineLocator engineLocator)
        {
            var handlerFactory = new HandlerFactory(handlerMappings, this.objectFactory, engineLocator);
            return new Processor(handlerFactory);
        }
    }
}