﻿namespace Kola.Nancy.Extensions
{
    using System.Collections.Generic;

    using Kola.Domain.Instances;

    using global::Nancy.ViewEngines.Razor;

    public static class HtmlHelpersExtensions
    {
        public static IHtmlString RenderPage<T>(this HtmlHelpers<T> helpers, PageInstance page)
        {
            var result = NancyKolaConfigurationRegistry.Instance.Renderer.Render(page);
            return new ResultWrapper(result, new NancyRazorViewHelper<T>(helpers));
        }

        public static IHtmlString RenderComponents<T>(this HtmlHelpers<T> helpers, IEnumerable<ComponentInstance> components)
        {
            var result = NancyKolaConfigurationRegistry.Instance.Renderer.Render(components);
            return new ResultWrapper(result, new NancyRazorViewHelper<T>(helpers));
        }

        public static IHtmlString RenderComponent<T>(this HtmlHelpers<T> helpers, ComponentInstance component)
        {
            var result = component.Render(NancyKolaConfigurationRegistry.Instance.Renderer);
            return new ResultWrapper(result, new NancyRazorViewHelper<T>(helpers));
        }
    }
}