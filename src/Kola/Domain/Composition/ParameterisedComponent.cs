﻿namespace Kola.Domain.Composition
{
    using System.Collections.Generic;

    using Kola.Domain.Extensions;
    using Kola.Domain.Instances;
    using Kola.Domain.Instances.Building;
    using Kola.Domain.Specifications;

    public abstract class ParameterisedComponent : IParameterisedComponent
    {
        private readonly List<Parameter> parameters = new List<Parameter>();

        protected ParameterisedComponent(string name, IEnumerable<Parameter> parameters)
        {
            this.Name = name;

            if (parameters != null)
            {
                this.parameters.AddRange(parameters);
            }
        }

        public string Name { get; private set; }

        public IEnumerable<Parameter> Parameters
        {
            get { return this.parameters; }
        }

        public Parameter AddParameter(string parameterName, IParameterisedComponentSpecification<IParameterisedComponent> specification)
        {
            var parameter = specification.Parameters.Find(parameterName).Create();
            this.parameters.Add(parameter);

            return parameter;
        }

        public abstract void Accept(IComponentVisitor visitor);

        public abstract T Accept<T, TContext>(IComponentVisitor<T, TContext> visitor, TContext context);

        public abstract IComponentInstance Build(IBuildContext buildContext);
    }
}