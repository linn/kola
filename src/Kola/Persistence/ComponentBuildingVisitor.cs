﻿namespace Kola.Persistence
{
    using System.Collections.Generic;

    using Kola.Domain;
    using Kola.Persistence.Extensions;
    using Kola.Persistence.Surrogates;

    public class ComponentBuildingVisitor : IComponentSurrogateVisitor
    {
        private readonly List<IComponent> components = new List<IComponent>();

        public IEnumerable<IComponent> Components
        {
            get { return this.components; }
        }

        public void Visit(CompositeComponentSurrogate surrogate)
        {
            this.components.Add(surrogate.ToDomain());
        }

        public void Visit(SimpleComponentSurrogate surrogate)
        {
            this.components.Add(surrogate.ToDomain());
        }
    }
}