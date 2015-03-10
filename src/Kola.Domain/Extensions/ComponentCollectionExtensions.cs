﻿namespace Kola.Domain.Extensions
{
    using System.Collections.Generic;

    using Kola.Domain.Composition;

    public static class ComponentCollectionExtensions
    {
        public static IComponentCollection FindCollection(this Template template, IEnumerable<int> path)
        {
            return new CollectionFindingComponentVisitor().Find(template, path);
        }

        public static IComponent FindComponent(this Template template, IEnumerable<int> path)
        {
            return new ComponentFindingComponentVisitor().Find(template, path);
        }

        public static IComponentWithProperties FindComponentWithProperties(this Template template, IEnumerable<int> path)
        {
            var candidate = new ComponentFindingComponentVisitor().Find(template, path);

            if (!(candidate is IComponentWithProperties))
            {
                throw new KolaException("Component with properties not found");
            }

            return candidate as IComponentWithProperties;
        }

        public static IEnumerable<T> FindAll<T>(this IComponentCollection collection) where T : class
        {
            var collectionAsType = collection as T;
            if (collectionAsType != null)
            {
                yield return collectionAsType;
            }

            foreach (var component in collection.Components)
            {
                var componentAsType = component as T;
                if (componentAsType != null)
                {
                    yield return componentAsType;
                }

                var childCollection = component as IComponentCollection;
                if (childCollection != null)
                {
                    foreach (var result in childCollection.FindAll<T>())
                    {
                        yield return result;
                    }
                }
            }
        }
    }
}