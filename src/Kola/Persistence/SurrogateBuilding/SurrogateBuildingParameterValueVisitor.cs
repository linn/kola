﻿namespace Kola.Persistence.SurrogateBuilding
{
    using System;

    using Kola.Domain.Composition.ParameterValues;
    using Kola.Persistence.Surrogates.ParameterValues;

    internal class SurrogateBuildingParameterValueVisitor : IParameterValueVisitor<ParameterValueSurrogate>
    {
        public ParameterValueSurrogate Visit(FixedParameterValue fixedParameterValue)
        {
            return new FixedParameterValueSurrogate
                {
                    Value = fixedParameterValue.Value
                };
        }

        public ParameterValueSurrogate Visit(InheritedParameterValue inheritedParameterValue)
        {
            return new InheritedParameterValueSurrogate();
        }

        public ParameterValueSurrogate Visit(MultilingualParameterValue multilingualParameterValue)
        {
            throw new NotImplementedException();
        }
    }
}