﻿namespace Unit.Tests.Temp.Domain.Templates
{
    using Unit.Tests.Temp.Domain.Instances;

    public class AtomTemplate : ITemplate, IComponent
    {
        public IInstance Build(IBuildContext buildContext)
        {
            return new AtomInstance();
        }
    }
}