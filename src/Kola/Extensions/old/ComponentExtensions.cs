﻿namespace Kola.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Editing;
    using Kola.Resources;

    internal static class ComponentExtensions
    {
        public static ComponentResource ToResource(this Component component, IEnumerable<string> templatePath, IEnumerable<int> componentIndices)
        {
            var composite = component as CompositeComponent;
            var components = (composite == null)
                                 ? Enumerable.Empty<ComponentResource>()
                                 : composite.Components.ToResource(templatePath, componentIndices);

            return new ComponentResource
            {
                Name = component.Name,

                Components = components,

                Links = new[]
                            {
                                new LinkResource
                                    {
                                        Rel = "self", 
                                        Href = templatePath.Append("_components").Concat(componentIndices.Select(i => i.ToString())).ToHttpPath() 
                                    },
                                new LinkResource
                                    {
                                        Rel = "componentPath", 
                                        Href = componentIndices.ToComponentPathString()
                                    }
                            }
            };
        }

        public static IEnumerable<ComponentResource> ToResource(this IEnumerable<Component> components, IEnumerable<string> templatePath, IEnumerable<int> componentIndices = null)
        {
            var result = new List<ComponentResource>();

            componentIndices = componentIndices ?? Enumerable.Empty<int>();

            for (var i = 0; i < components.Count(); i++)
            {
                result.Add(components.ElementAt(i).ToResource(templatePath, componentIndices.Append(i)));
            }

            return result;
        }
    }
}