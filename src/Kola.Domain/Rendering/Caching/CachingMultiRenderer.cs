﻿namespace Kola.Domain.Rendering.Caching
{
    using System;
    using System.Collections.Generic;

    using Kola.Domain.Instances;
    using Kola.Domain.Rendering;

    public class CachingMultiRenderer : IMultiRenderer
    {
        private readonly IMultiRenderer inner;
        private readonly ICacheManager cacheManager;

        public CachingMultiRenderer(IMultiRenderer inner, ICacheManager cacheManager)
        {
            this.inner = inner;
            this.cacheManager = cacheManager;
        }

        //public IResult Process(IComponentInstance component)
        //{
        //    return new CachingResult(this.inner.Process(component), this.cacheManager, component);
        //}

        public IResult Render(AtomInstance atom)
        {
            throw new NotImplementedException();
        }

        public IResult Render(ContainerInstance container)
        {
            throw new NotImplementedException();
        }

        public IResult Render(WidgetInstance widget)
        {
            throw new NotImplementedException();
        }

        public IResult Render(AreaInstance area)
        {
            throw new NotImplementedException();
        }

        public IResult Render(PageInstance page)
        {
            throw new NotImplementedException();
        }

        public IResult Render(IEnumerable<ComponentInstance> components)
        {
            throw new NotImplementedException();
        }
    }
}