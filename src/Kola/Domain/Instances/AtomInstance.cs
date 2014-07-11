﻿namespace Kola.Domain.Instances
{
    using System.Collections.Generic;

    using Kola.Domain.Rendering;

    public class AtomInstance : ComponentInstance
    {
        public AtomInstance(IEnumerable<int> path, string name, IEnumerable<ParameterInstance> parameters)
            : base(path)
        {
            this.Name = name;
            this.Parameters = parameters;
        }

        public string Name { get; private set; }

        public IEnumerable<ParameterInstance> Parameters { get; private set; }

        public override T Accept<T, TContext>(IComponentInstanceVisitor<T, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override IResult Render(IMultiRenderer renderer)
        {
            return renderer.Render(this);
        }
    }
}