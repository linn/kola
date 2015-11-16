﻿namespace Integration.Tests.Nancy.Modules.TemplatesModuleSpecs
{
    using global::Nancy.Testing;

    using Kola.Domain.Composition;
    using Kola.Nancy.Modules;
    using Kola.Nancy.Processors;
    using Kola.Persistence;
    using Kola.Service.ResourceBuilding;
    using Kola.Service.Services;

    using NUnit.Framework;

    using Rhino.Mocks;

    public abstract class ContextBase
    {
        protected Browser Browser { get; private set; }

        protected BrowserResponse Response { get; set; }

        protected IContentRepository ContentRepository { get; set; }

        protected IComponentSpecificationLibrary ComponentLibrary { get; set; }

        [SetUp]
        public void EstablishBaseContext()
        {
            this.ContentRepository = MockRepository.GenerateMock<IContentRepository>();
            this.ComponentLibrary = MockRepository.GenerateMock<IComponentSpecificationLibrary>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependencies(new object[] { this.ContentRepository, this.ComponentLibrary });
                        with.Dependency<TemplateResourceBuilder>();
                        with.Dependency<AmendmentResourceBuilder>();
                        with.Dependency<ComponentResourceBuilder>();
                        with.ResponseProcessor<TemplateResultProcessor>();
                        with.ResponseProcessor<AmendmentResultProcessor>();
                        with.ResponseProcessor<ComponentResultProcessor>();
                        with.Dependency<TemplateService>();
                        with.Module<TemplateModule>();
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}