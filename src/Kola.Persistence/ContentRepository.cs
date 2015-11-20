﻿namespace Kola.Persistence
{
    using System.Collections.Generic;
    using System.Configuration;

    using Kola.Domain.Composition;
    using Kola.Persistence.DomainBuilding;
    using Kola.Persistence.Extensions;
    using Kola.Persistence.SurrogateBuilding;
    using Kola.Persistence.Surrogates;

    internal class ContentRepository : IContentRepository
    {
        private const string TemplateFileName = "Template.xml";
        private const string RedirectFileName = "Redirect.xml";

        private readonly SerializationHelper serializationHelper;

        private readonly FileSystemHelper fileSystemHelper;

        private readonly string templatesDirectory;

        public ContentRepository(SerializationHelper serializationHelper, FileSystemHelper fileSystemHelper)
            : this(serializationHelper, fileSystemHelper, rootDirectory: ConfigurationManager.AppSettings["ContentRoot"])
        {
        }

        public ContentRepository(
            SerializationHelper serializationHelper,
            FileSystemHelper fileSystemHelper,
            string rootDirectory)
        {
            this.serializationHelper = serializationHelper;
            this.fileSystemHelper = fileSystemHelper;
            this.templatesDirectory = fileSystemHelper.CombinePaths(new[] { rootDirectory, "Templates" });

        }

        public void Add(IContent content)
        {
            var template = content as Template;
            if (template != null)
            {
                var surrogate = new TemplateSurrogateBuilder().Build(template);
                var directoryPath = this.fileSystemHelper.CombinePaths(
                    this.templatesDirectory,
                    template.Path.ToFileSystemPath());

                if (!this.fileSystemHelper.DirectoryExists(directoryPath))
                {
                    this.fileSystemHelper.CreateDirectory(directoryPath);
                }

                var path = this.fileSystemHelper.CombinePaths(directoryPath, TemplateFileName);
                this.serializationHelper.Serialize<TemplateSurrogate>(surrogate, path);
            }
        }

        public IEnumerable<IContent> FindContents(IEnumerable<string> path)
        {
            var directoryPath = this.fileSystemHelper.CombinePaths(this.templatesDirectory, path.ToFileSystemPath());
            var redirectPath = this.fileSystemHelper.CombinePaths(directoryPath, RedirectFileName);
            var templatePath = this.fileSystemHelper.CombinePaths(directoryPath, TemplateFileName);

            if (this.fileSystemHelper.FileExists(redirectPath))
            {
                var surrogate = this.serializationHelper.Deserialize<RedirectSurrogate>(redirectPath);
                yield return new RedirectDomainBuilder().Build(surrogate);
            }

            if (this.fileSystemHelper.FileExists(templatePath))
            {
                var surrogate = this.serializationHelper.Deserialize<TemplateSurrogate>(templatePath);
                yield return new TemplateDomainBuilder(path).Build(surrogate);
            }
        }

        public void Update(IContent content)
        {
            var template = content as Template;
            if (template != null)
            {
                var surrogate = new TemplateSurrogateBuilder().Build(template);
                var path = this.fileSystemHelper.CombinePaths(
                    this.templatesDirectory,
                    template.Path.ToFileSystemPath(),
                    TemplateFileName);
                this.serializationHelper.Serialize<TemplateSurrogate>(surrogate, path);
            }
        }
    }
}