﻿namespace Kola.Domain.Rendering
{
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Domain.Instances;

    public class Renderer : IRenderer
    {
        private readonly IHandlerFactory handlerFactory;

        public Renderer(IHandlerFactory handlerFactory)
        {
            this.handlerFactory = handlerFactory;
        }

        public IResult Render(AtomInstance atom)
        {
            return this.handlerFactory.GetAtomHandler(atom.Name).Render(atom);
        }

        public IResult Render(ContainerInstance container)
        {
            return this.handlerFactory.GetContainerHandler(container.Name).Render(container);
        }

        public IResult Render(WidgetInstance widget)
        {
            return new CompositeResult(widget.Components.Select(c => c.Render(this)));
        }

        public IResult Render(PlaceholderInstance placeholder)
        {
            return new CompositeResult(placeholder.Components.Select(c => c.Render(this)));
        }

        public IResult Render(PageInstance page)
        {
            return new CompositeResult(page.Components.Select(c => c.Render(this)));
        }

        public IResult Render(IEnumerable<IComponentInstance> components)
        {
            return new CompositeResult(components.Select(c => c.Render(this)));
        }
    }
}