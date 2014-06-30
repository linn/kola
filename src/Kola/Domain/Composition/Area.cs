﻿namespace Kola.Domain.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Domain.Instances;
    using Kola.Domain.Instances.Building;

    public class Area : IComponentCollection, IComponent
    {
        private readonly List<IComponent> components = new List<IComponent>();

        public Area(IEnumerable<IComponent> components = null)
        {
            if (components != null)
            {
                this.components.AddRange(components);
            }
        }

        public IEnumerable<IComponent> Components
        {
            get { return this.components; }
        }

        public void Insert(int index, IComponent component)
        {
            if (index > this.components.Count)
            {
                throw new KolaException("Specified index outwith bounds of component collection");
            }

            this.components.Insert(index, component);
        }

        public void RemoveAt(int index)
        {
            this.components.RemoveAt(index);
        }

        public T Accept<T>(IComponentVisitor<T> visitor)
        {
            throw new NotImplementedException();
        }

        public T Accept<T, TContext>(IComponentVisitor<T, TContext> visitor, TContext context)
        {
            throw new NotImplementedException();
        }

        public IComponentInstance Build(IBuildContext buildContext)
        {
            return new AreaInstance(this.Components.Select(c => c.Build(buildContext)).ToList());
        }
    }
}