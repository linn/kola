﻿namespace Kola.Configuration.Fluent
{
    using Kola.Domain;
    using Kola.Rendering;

    public class ComponentConfigurer
    {
        private readonly IPluginComponentSpecification<IComponent> specification;

        internal ComponentConfigurer(IPluginComponentSpecification<IComponent> specification)
        {
            this.specification = specification;
        }

        public ComponentHandlerConfigurer WithHandler<T>(string viewName = "")
        {
            this.specification.HandlerType = typeof(T);
            this.specification.ViewName = viewName;
            return new ComponentHandlerConfigurer(this.specification);
        }

        public ComponentHandlerConfigurer WithView(string viewName)
        {
            return this.WithHandler<DefaultHandler>(viewName);
        }
    }
}