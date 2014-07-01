﻿namespace Kola.Domain.Rendering.Caching
{
    using Kola.Domain.Instances;
    using Kola.Domain.Rendering;

    public class CachingResult : IResult
    {
        private readonly IResult inner;
        private readonly ICacheManager cacheManager;
        private readonly ComponentInstance component;

        public CachingResult(IResult inner, ICacheManager cacheManager, ComponentInstance component)
        {
            this.inner = inner;
            this.cacheManager = cacheManager;
            this.component = component;
        }

        public string ToHtml(IViewHelper viewHelper)
        {
            var result = this.inner.ToHtml(viewHelper);

            this.cacheManager.Record(this.component, result);

            return result;
        }
    }
}