﻿namespace Kola.Domain.Instances
{
    using System.Collections.Generic;

    using Kola.Domain.Rendering;

    public abstract class ComponentInstance
    {
        protected ComponentInstance(IEnumerable<int> path, IRenderingInstructions renderingInstructions)
        {
            this.Path = path;
            this.RenderingInstructions = renderingInstructions;
        }

        public IEnumerable<int> Path { get; private set; }

        public IRenderingInstructions RenderingInstructions { get; private set; }

        public abstract T Accept<T, TContext>(IComponentInstanceVisitor<T, TContext> visitor,  TContext context);

        public abstract IResult Render(IMultiRenderer renderer);
    }
}