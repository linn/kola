﻿namespace Kola.Service.ResourceBuilding
{
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Domain.Composition;
    using Kola.Resources;
    using Kola.Service.Extensions;

    public class TemplateResourceBuilder : IResourceBuilder<Template>
    {
        public object Build(Template template)
        {
            var visitor = new ResourceBuildingComponentVisitor(template);

            return new TemplateResource
            {
                Components = template.Components.Select((c, i) => c.Accept(visitor, new[] { i })),
                Links = this.GetLinks(template).ToArray()
            };
        }

        public string Location(Template template)
        {
            return $"/_kola/templates?templatePath={template.Path.ToHttpPath()}";
        }

        private IEnumerable<LinkResource> GetLinks(Template template)
        {
            yield return new LinkResource
            {
                Rel = "self",
                Href = $"/_kola/templates?templatePath={template.Path.ToHttpPath()}"
            };

            yield return new LinkResource
            {
                Rel = "amendments",
                Href = $"/_kola/templates/amendments?templatePath={template.Path.ToHttpPath()}"
            };

            foreach (var previewUrl in template.InstancePaths ?? Enumerable.Empty<string>())
            {
                yield return new LinkResource
                {
                    Rel = "preview",
                    Href = previewUrl
                };
            }
        }
   }
}