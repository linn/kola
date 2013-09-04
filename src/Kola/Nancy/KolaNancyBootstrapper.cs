﻿namespace Kola.Nancy
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Kola.Configuration.Ideas;

    using global::Nancy;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Conventions;
    using global::Nancy.Embedded.Conventions;
    using global::Nancy.TinyIoc;
    using global::Nancy.ViewEngines;
    using global::Nancy.ViewEngines.Razor;

    //using global::Nancy.Serializers.Json.ServiceStack;

    //using ServiceStack.Text;

    public class KolaNancyBootstrapper : DefaultNancyBootstrapper
    {

        // TODO : A better way of adding plugins to AppDomainAssemblyTypeScanner is required :)
        public static Func<Assembly, bool>[] CandidatePluginAssemblies = new Func<Assembly, bool>[]
        {
            x => x.GetReferencedAssemblies().Any(r => r.Name.StartsWith("Kola"))
        };

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(c =>
                {
                    var i = c.Serializers.Count();
                    //c.Serializers.Insert(0, typeof(global::Nancy.Serializers.Json.ServiceStack.ServiceStackJsonSerializer));
                    //if (c.Serializers.Count() == 0 || c.Serializers.OfType<ServiceStackJsonSerializer>().Count() == 0)
                    //    c.Serializers.Insert(0, typeof(ServiceStackJsonSerializer));
                    c.ViewLocationProvider = typeof(ResourceViewLocationProvider);
                });
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            var kolaHostConfiguration = KolaBootstrapper.Bootstrap(new TinyIoCObjectFactory(container));

            foreach (var viewLocation in kolaHostConfiguration.ViewLocations)
            {
                ResourceViewLocationProvider.RootNamespaces.Add(viewLocation.Assembly, viewLocation.Location);
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

            //JsConfig.DateHandler = JsonDateHandler.ISO8601;
            //JsConfig.EmitCamelCaseNames = true;
            //JsConfig.ExcludeTypeInfo = true;
            //JsConfig<Guid>.SerializeFn = g => g.ToString(); // otherwise it excludes hypens 
            //JsConfig<Guid?>.SerializeFn = g => g.Value.ToString(); // otherwise it excludes hypens
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(EmbeddedStaticContentConventionBuilder.AddDirectory("/_kola/Scripts", typeof(KolaNancyBootstrapper).Assembly, "/Scripts"));
            conventions.StaticContentsConventions.Add(EmbeddedStaticContentConventionBuilder.AddDirectory("/_kola/Content", typeof(KolaNancyBootstrapper).Assembly, "/Content"));
        }
    }
}