﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kola.Processing;

/*
 * Here's what we are going to do:
 * 
 * 1 - Make a bootstrapper that has generic knowledge of an IOC container
 * 2 - The bootstrapper builds the configuration and pops it into the container
 * 3 - The configuration is then available from the container
 * 4 - The bootstrapper is the one who does all the assembly scanning stuff
 * 5 - The configuration holds the handlers for all of the 
 */
namespace Kola.Configuration.Ideas
{
    public static class KolaBootstrapper
    {
        public static KolaHostConfiguration Bootstrap(IObjectFactory objectFactory)
        {
            var kolaConfiguration = new KolaHostConfiguration();
            var handlerMappings = new Dictionary<string, Type>();

            foreach (var plugin in FindPlugins())
            {
                kolaConfiguration.AddPlugInConfiguration(plugin, plugin.GetType().Assembly);

                foreach (var atom in plugin.ComponentConfigurations)
                {
                    handlerMappings.Add(atom.Name, atom.HandlerType);
                }
            }

            KolaRegistry.KolaEngine = new KolaEngine(new KolaEngineConfiguration(handlerMappings, objectFactory));

            return kolaConfiguration;
        }

        private static IEnumerable<PluginConfiguration> FindPlugins()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(FindPlugins);

            foreach (var type in types)
            {
                yield return (PluginConfiguration)type.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
        }

        private static IEnumerable<Type> FindPlugins(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => (t != typeof(PluginConfiguration)) && typeof(PluginConfiguration).IsAssignableFrom(t));
        }
    }
}
