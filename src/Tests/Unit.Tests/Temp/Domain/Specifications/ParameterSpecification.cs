﻿namespace Unit.Tests.Temp.Domain.Specifications
{
    using Unit.Tests.Temp.Domain.Templates;

    public class ParameterSpecification : ISpecification<ParameterTemplate>
    {
        public ParameterTemplate Create()
        {
            return new ParameterTemplate();
        }
    }
}