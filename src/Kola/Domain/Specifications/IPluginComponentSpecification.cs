﻿namespace Kola.Domain.Specifications
{
    using System;
    using System.Collections.Generic;

    using Kola.Domain.Templates;

    public interface IPluginComponentSpecification<out T> : IComponentSpecification<T>
        where T : IComponent
    {
        Type HandlerType { get; set; }

        CacheType CacheType { get; set; }

        int CacheDuration { get; set; }

        string ViewName { get; set;  }

        IEnumerable<ParameterSpecification> Parameters { get; }

        void AddParameter(ParameterSpecification parameter);
    }
}