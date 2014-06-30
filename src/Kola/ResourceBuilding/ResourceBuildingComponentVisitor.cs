﻿namespace Kola.ResourceBuilding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Domain.Composition;
    using Kola.Extensions;
    using Kola.Resources;

    internal class ResourceBuildingComponentVisitor : IComponentVisitor<ComponentResource, IEnumerable<int>>
    {
        private readonly IEnumerable<string> templatePath;
        private readonly ResourceBuildingParameterValueVisitor parameterValueBuilder = new ResourceBuildingParameterValueVisitor();

        public ResourceBuildingComponentVisitor(IEnumerable<string> templatePath)
        {
            this.templatePath = templatePath;
        }

        public ComponentResource Visit(Atom atom, IEnumerable<int> context)
        {
            return new AtomResource
                {
                    Name = atom.Name,
                    Path = context.Select(i => i.ToString()).ToHttpPath(),
                    Parameters = this.BuildParameters(atom.Parameters),
                    Links = this.BuildLinks(context)
                };
        }

        public ComponentResource Visit(Container container, IEnumerable<int> context)
        {
            return new ContainerResource
                {
                    Name = container.Name,
                    Path = context.Select(i => i.ToString()).ToHttpPath(),
                    Components = container.Components.Select((c, i) => c.Accept(this, context.Append(i))),
                    Parameters = this.BuildParameters(container.Parameters),
                    Links = this.BuildLinks(context)
                };
        }

        public ComponentResource Visit(Widget widget, IEnumerable<int> context)
        {
            return new WidgetResource
                {
                    Name = widget.Name,
                    Areas = widget.Areas.Select((c, i) => c.Accept(this, context.Append(i))),
                    Path = context.Select(i => i.ToString()).ToHttpPath(),
                    Parameters = this.BuildParameters(widget.Parameters),
                    Links = this.BuildLinks(context)
                };
        }

        public ComponentResource Visit(Placeholder placeholder, IEnumerable<int> context)
        {
            return new PlaceholderResource
                {
                    Path = context.Select(i => i.ToString()).ToHttpPath(),
                    Links = this.BuildLinks(context)
                };
        }

        public ComponentResource Visit(Area area, IEnumerable<int> context)
        {
            return new AreaResource
            {
                Path = context.Select(i => i.ToString()).ToHttpPath(),
                Components = area.Components.Select((c, i) => c.Accept(this, context.Append(i))),
                Links = this.BuildLinks(context)
            };
        }

        private IEnumerable<ParameterResource> BuildParameters(IEnumerable<Parameter> parameters)
        {
            return parameters.Select(parameter => new ParameterResource
                {
                    Name = parameter.Name,
                    Type = parameter.Type,
                    Value = parameter.Value == null ? null : parameter.Value.Accept(this.parameterValueBuilder)
                });
        }

        private IEnumerable<LinkResource> BuildLinks(IEnumerable<int> context)
        {
            yield return new LinkResource
                {
                    Rel = "self",
                    Href = new[] { "_kola", "templates" }.Concat(this.templatePath).Append("_components").Concat(context.Select(i => i.ToString())).ToHttpPath()
                };
        }
    }
}