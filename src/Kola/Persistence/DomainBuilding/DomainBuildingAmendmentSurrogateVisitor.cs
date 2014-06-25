﻿namespace Kola.Persistence.DomainBuilding
{
    using Kola.Domain.Composition.Amendments;
    using Kola.Extensions;
    using Kola.Persistence.Surrogates.Amendments;

    internal class DomainBuildingAmendmentSurrogateVisitor : IAmendmentSurrogateVisitor<IAmendment>
    {
        public IAmendment Visit(AddComponentAmendmentSurrogate surrogate)
        {
            return new AddComponentAmendment(surrogate.TargetPath.ParseComponentPath(), surrogate.ComponentType);
        }

        public IAmendment Visit(MoveComponentAmendmentSurrogate surrogate)
        {
            return new MoveComponentAmendment(surrogate.SourcePath.ParseComponentPath(), surrogate.TargetPath.ParseComponentPath());
        }

        public IAmendment Visit(RemoveComponentAmendmentSurrogate surrogate)
        {
            return new RemoveComponentAmendment(surrogate.ComponentPath.ParseComponentPath());
        }
    }
}
