﻿namespace Kola.Nancy.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Domain;
    using Kola.Resources;

    internal static class ComponentExtensions
    {
        public static ComponentResource ToResource(this Component component, IEnumerable<int> componentIndices)
        {
            var componentPath = string.Join("/", componentIndices);
            return new ComponentResource
            {
                Name = component.Name,

                Components = component.Components.ToResource(componentIndices),

                Links = new[]
                            {
                                new LinkResource
                                    {
                                        Rel = "componentPath", 
                                        Href = componentPath
                                    }
                            }
            };
        }

        public static IEnumerable<ComponentResource> ToResource(this IEnumerable<Component> components, IEnumerable<int> componentIndices = null)
        {
            var result = new List<ComponentResource>();

            componentIndices = componentIndices ?? Enumerable.Empty<int>();

            for (var i = 0; i < components.Count(); i++)
            {
                result.Add(components.ElementAt(i).ToResource(componentIndices.Append(i)));
            }

            return result;
        }
    }
}