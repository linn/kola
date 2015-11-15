﻿namespace Integration.Tests.Nancy.Modules.RenderingModuleSpecs
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using global::Nancy;

    using Kola.Configuration;
    using Kola.Domain.Rendering;
    using Kola.Nancy.Modules;

    using global::Nancy.Testing;
    using global::Nancy.ViewEngines;
    using global::Nancy.ViewEngines.Razor;

    using Kola.Domain.Composition;
    using Kola.Nancy;
    using Kola.Nancy.Processors;
    using Kola.Service.ResourceBuilding;
    using Kola.Service.Services;

    using NUnit.Framework;

    using Rhino.Mocks;

    public abstract class ContextBase
    {
        protected Browser Browser { get; private set; }

        protected BrowserResponse Response { get; set; }

        protected IRenderingService RenderingService { get; set; }

        protected IRendererFactory HandlerFactory { get; set; }

        [SetUp]
        public void EstablishBaseContext()
        {
            this.RenderingService = MockRepository.GenerateMock<IRenderingService>();
            this.HandlerFactory = MockRepository.GenerateMock<IRendererFactory>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.RenderingService);
                        with.Module<RenderingModule>();
                        with.ViewEngine<RazorViewEngine>();
                        with.ViewLocationProvider<ResourceViewLocationProvider>();

                        ResourceViewLocationProvider.RootNamespaces.Clear();
                        ResourceViewLocationProvider.RootNamespaces.Add(typeof(KolaNancyBootstrapper).Assembly, "Kola.Nancy");
                    });

            KolaConfigurationRegistry.Register(new KolaConfiguration(new MultiRenderer(this.HandlerFactory), null));
            this.Browser = new Browser(bootstrapper);
        }
    }
}