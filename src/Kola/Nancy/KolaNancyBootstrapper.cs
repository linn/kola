﻿namespace Kola.Nancy
{
    using System;
    using System.Linq;
    using System.Reflection;

    using global::Nancy;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Conventions;
    using global::Nancy.Embedded.Conventions;
    using global::Nancy.TinyIoc;
    using global::Nancy.ViewEngines;
    using global::Nancy.ViewEngines.Razor;

    using Kola.Configuration;

    using ServiceStack.Text;

    public class KolaNancyBootstrapper : DefaultNancyBootstrapper
    {
        // TODO : A better way of adding plugins to AppDomainAssemblyTypeScanner is required :)
        private static readonly Func<Assembly, bool>[] CandidatePluginAssemblies = new Func<Assembly, bool>[]
        {
            x => x.GetReferencedAssemblies().Any(r => r.Name.StartsWith("Kola"))
        };

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(c =>
                    {
                        c.ViewLocationProvider = typeof(ResourceViewLocationProvider);
                    });
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            var configurationBuilder = new DefaultKolaConfigurationBuilder(new PluginFinder(), new TinyIoCObjectFactory(container));
            KolaConfiguration kolaConfiguration = configurationBuilder.Build();
            //TODO {SC} Us ethe IOC container to hold the Kola configuration
            NancyKolaRegistry.KolaConfiguration = kolaConfiguration;

            foreach (var pluginSummary in kolaConfiguration.PluginSummaries)
            {
                ResourceViewLocationProvider.RootNamespaces.Add(pluginSummary.Assembly, pluginSummary.ViewLocation);
            }

            ResourceViewLocationProvider.RootNamespaces.Add(typeof(KolaNancyBootstrapper).Assembly, "Kola.Nancy");
            ResourceViewLocationProvider.Ignore.Add(typeof(RazorViewEngine).Assembly);
            ResourceViewLocationProvider.Ignore.Add(Assembly.Load("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
            AppDomainAssemblyTypeScanner.AddAssembliesToScan(AppDomainAssemblyTypeScanner.DefaultAssembliesToScan.Concat(CandidatePluginAssemblies).ToArray());

            base.ConfigureApplicationContainer(container);
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            //Elmahlogging.Enable(pipelines, "elmah");

            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.EmitCamelCaseNames = true;
            JsConfig.ExcludeTypeInfo = true;
            JsConfig<Guid>.SerializeFn = g => g.ToString(); // otherwise it excludes hypens 
            JsConfig<Guid?>.SerializeFn = g => g.Value.ToString(); // otherwise it excludes hypens
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            conventions.StaticContentsConventions.Add(EmbeddedStaticContentConventionBuilder.AddDirectory("/_kola/Scripts", typeof(KolaNancyBootstrapper).Assembly, "/Scripts"));
            conventions.StaticContentsConventions.Add(EmbeddedStaticContentConventionBuilder.AddDirectory("/_kola/Content", typeof(KolaNancyBootstrapper).Assembly, "/Content"));
        }
    }
}