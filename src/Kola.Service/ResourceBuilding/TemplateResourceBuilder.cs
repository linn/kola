﻿namespace Kola.Service.ResourceBuilding
{
    using System.Linq;

    using Kola.Domain.Composition;
    using Kola.Resources;
    using Kola.Service.Extensions;

    public class TemplateResourceBuilder
    {
        public TemplateResource Build(Template template)
        {
            var visitor = new ResourceBuildingComponentVisitor(template.Path);

            return new TemplateResource
            {
                Components = template.Components.Select((c, i) => c.Accept(visitor, new[] { i })),
                Links = new[]
                            {
                                new LinkResource
                                    {
                                        Rel = "self",
                                        Href = new[] { "_kola", "templates" }.Concat(template.Path).ToHttpPath()
                                    },
                                new LinkResource
                                    {
                                        Rel = "preview",
                                        Href = template.Path.ToHttpPath() + "?preview=y"
                                    },
                                new LinkResource
                                    {
                                        Rel = "amendments",
                                        Href = new[] { "_kola", "templates" }.Concat(template.Path).Append("_amendments").ToHttpPath()
                                    }
                            }
            };
        }
    }
}