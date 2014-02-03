﻿namespace Unit.Tests.Temp.Domain.Instances
{
    using System.Collections.Generic;

    public class WidgetInstance : IInstance
    {
        public WidgetInstance(IEnumerable<IInstance> components)
        {
            this.Components = components;
        }

        public IEnumerable<IInstance> Components { get; private set; }
    }
}
