﻿namespace Integration.Tests.Nancy.Modules.RenderingModuleSpecs
{
    using Kola.Configuration;
    using Kola.Domain.Rendering;
    using Kola.Nancy.Modules;

    using global::Nancy.Testing;
    using global::Nancy.ViewEngines;

    using NUnit.Framework;

    using Rhino.Mocks;

    public abstract class ContextBase
    {
        protected Browser Browser { get; private set; }

        protected BrowserResponse Response { get; set; }

        protected IPageHandler PageHandler { get; set; }

        protected IRendererFactory HandlerFactory { get; set; }

        [SetUp]
        public void EstablishBaseContext()
        {
            this.PageHandler = MockRepository.GenerateMock<IPageHandler>();
            this.HandlerFactory = MockRepository.GenerateMock<IRendererFactory>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        ResourceViewLocationProvider.RootNamespaces.Clear();
                        ResourceViewLocationProvider.RootNamespaces.Add(typeof(IPageHandler).Assembly, "Kola.Nancy.Views");

                        with.Dependency(this.PageHandler);
                        with.Module<RenderingModule>();
                        with.ViewLocationProvider(new ResourceViewLocationProvider());
                    });

            KolaConfigurationRegistry.Register(new KolaConfiguration(new MultiRenderer(this.HandlerFactory), null));
            this.Browser = new Browser(bootstrapper);
        }
    }
}