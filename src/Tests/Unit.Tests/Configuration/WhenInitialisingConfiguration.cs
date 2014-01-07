﻿namespace Unit.Tests.Configuration
{
    using FluentAssertions;

    using Kola.Configuration;
    using Kola.Configuration.Plugins;
    using Kola.Processing;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class WhenInitialisingConfiguration
    {
        private KolaConfiguration configuration;

        [SetUp]
        public void EstablishContext()
        {
            var plugin1Configuration = MockRepository.GenerateStub<PluginConfiguration>();
            var plugin2Configuration = MockRepository.GenerateStub<PluginConfiguration>();
            var pluginFinder = MockRepository.GenerateStub<IPluginFinder>();
            var objectFactory = MockRepository.GenerateStub<IObjectFactory>();

            pluginFinder.Stub(f => f.FindPlugins()).Return(new[] { plugin1Configuration, plugin2Configuration });

            var builder = new DefaultKolaConfigurationBuilder(pluginFinder, objectFactory);

            this.configuration = builder.Build();
        }

        [Test]
        public void TheKolaEngineShouldBeSet()
        {
            this.configuration.KolaEngine.Should().NotBeNull();
        }

        [Test]
        public void TheListOfPluginSummariesShouldBeSet()
        {
            this.configuration.PluginSummaries.Should().HaveCount(2);
        }
    }
}