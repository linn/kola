﻿namespace Unit.Tests.Temp.Domain.Templates
{
    using System;
    using System.Collections.Generic;

    using Unit.Tests.Temp.Domain.Instances;

    public class ContainerTemplate : ITemplate, IComponent, IContainer
    {
        public IEnumerable<IComponent> Children
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IInstance Build(IBuildContext buildContext)
        {
            return new ContainerInstance();
        }
    }
}