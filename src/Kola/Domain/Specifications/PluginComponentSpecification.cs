﻿namespace Kola.Domain.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Kola.Domain.Templates;
    using Kola.Domain.Templates.ParameterValues;

    public abstract class PluginComponentSpecification<T> : IPluginComponentSpecification<T>
        where T : IComponentTemplate
    {
        private readonly List<ParameterSpecification> parameters = new List<ParameterSpecification>();

        protected PluginComponentSpecification(string name)
        {
            this.Name = name;
        }

        public Type RendererType { get; set; }

        public string Name { get; private set; }

        public CacheType CacheType { get; set; }

        public int CacheDuration { get; set; }

        public string ViewName { get; set; }

        public IEnumerable<ParameterSpecification> Parameters
        {
            get { return this.parameters; }
        }

        public void AddParameter(ParameterSpecification parameter)
        {
            this.parameters.Add(parameter);
        }

        public abstract T Create();

        protected IEnumerable<ParameterTemplate> CreateParameters()
        {
            return this.Parameters.Select(p => p.Create()).Where(p => !(p.Value is UndefinedParameterValue));
        }
    }
}