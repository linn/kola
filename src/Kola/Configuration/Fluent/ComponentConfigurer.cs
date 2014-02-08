﻿namespace Kola.Configuration.Fluent
{
    using Kola.Domain.Rendering;
    using Kola.Domain.Specifications;
    using Kola.Domain.Templates;

    public class ComponentConfigurer
    {
        private readonly IPluginComponentSpecification<IComponentTemplate> specification;

        internal ComponentConfigurer(IPluginComponentSpecification<IComponentTemplate> specification)
        {
            this.specification = specification;
        }

        public ComponentRendererConfigurer WithRenderer<T>(string viewName = "")
        {
            this.specification.RendererType = typeof(T);
            this.specification.ViewName = viewName;
            return new ComponentRendererConfigurer(this.specification);
        }

        public ComponentRendererConfigurer WithView(string viewName)
        {
            return this.WithRenderer<DefaultRenderer>(viewName);
        }
    }
}