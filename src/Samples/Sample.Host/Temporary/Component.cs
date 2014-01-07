﻿namespace Sample.Host.Temporary
{
    using System.Collections.Generic;

    using Kola.Processing;

    public class Component : IComponent
    {
        public string Name { get; set; }

        public IEnumerable<IComponent> Children { get; set; }
    }
}