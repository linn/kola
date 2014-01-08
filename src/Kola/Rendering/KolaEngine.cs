﻿namespace Kola.Rendering
{
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Domain;

    public class KolaEngine
    {
        private readonly IProcessor processor;

        public KolaEngine(IProcessor processor)
        {
            this.processor = processor;
        }

        public IResult Render(IPage page)
        {
            var results = page.Components.Select(this.processor.Process);

            return new CompositeResult(results);
        }

        public IResult Render(IEnumerable<IComponentInstance> children)
        {
            var results = children.Select(this.processor.Process);

            return new CompositeResult(results);
        }
    }
}