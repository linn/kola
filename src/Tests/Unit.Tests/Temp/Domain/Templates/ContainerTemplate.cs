﻿namespace Unit.Tests.Temp.Domain.Templates
{
    using Unit.Tests.Temp.Domain.Instances;

    public class ContainerTemplate : ITemplate
    {
        public IInstance Build(IBuildContext buildContext)
        {
            return new ContainerInstance();
        }
    }
}