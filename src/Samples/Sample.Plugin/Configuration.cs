﻿namespace Sample.Plugin
{
    using Kola.Configuration.Plugins;

    using Sample.Plugin.Renderers;
    using Sample.Plugin.Sources;

    public class Configuration : PluginConfiguration
    {
        public Configuration()
            : base("Sample")
        {
            this.Configure.ViewLocation("Sample.Plugin.Views");

            this.ConfigureAtoms();

            this.ConfigureContainers();

            this.ConfigurePropertyTypes();

            this.ConfigureSources();
        }

        private void ConfigureAtoms()
        {
            this.Configure.Atom("artist")
                .WithRenderer<ArtistRenderer>()
                .WithProperty("artist-id", "text");

            this.Configure.Atom("artist-image")
                .WithRenderer<ArtistRenderer>()
                .WithProperty("artist-id", "text");

            this.Configure.Atom("album")
                .WithRenderer<AlbumRenderer>()
                .WithProperty("album-id", "text");

            this.Configure.Atom("album-art")
                .WithRenderer<AlbumRenderer>()
                .WithProperty("album-id", "text");
        }

        private void ConfigureContainers()
        {
            this.Configure.Container("div")
                .WithView("Div")
                .WithProperty("classes", "text");
        }

        private void ConfigurePropertyTypes()
        {
        }

        private void ConfigureSources()
        {
            this.Configure.Source<AlbumNameSource>();
            this.Configure.Source<ArtistNameSource>();
        }
    }
}
