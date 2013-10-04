﻿namespace Kola.Nancy.Modules
{
    using Kola.Extensions;
    using Kola.Persistence;

    using global::Nancy;

    public class ComponentTypeModule : NancyModule
    {
        private readonly IComponentTypeRepository componentTypeRepository;

        public ComponentTypeModule(IComponentTypeRepository componentTypeRepository)
        {
            this.componentTypeRepository = componentTypeRepository;
            this.Get["/_kola/component-types", AcceptHeaderFilters.NotHtml] = p => this.GetComponentTypes();
        }

        private dynamic GetComponentTypes()
        {
            var componentTypes = this.componentTypeRepository.FindAll();

            var result = this.Response.AsJson(componentTypes.ToResource());

            return result;
        }
    }
}