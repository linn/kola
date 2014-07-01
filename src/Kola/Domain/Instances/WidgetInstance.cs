﻿namespace Kola.Domain.Instances
{
    using System.Collections.Generic;

    using Kola.Domain.Rendering;

    public class WidgetInstance : ComponentInstance
    {
        public WidgetInstance(IEnumerable<int> path, IEnumerable<ComponentInstance> components = null)
            : base(path)
        {
            this.Components = components;
        }

        public IEnumerable<ComponentInstance> Components { get; private set; }

        public override IResult Render(IRenderer renderer)
        {
            return renderer.Render(this);
        }
    }
}