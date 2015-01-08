﻿namespace Kola.DomainBuilding
{
    using Kola.Domain.Composition.Amendments;
    using Kola.Extensions;
    using Kola.Resources;

    internal class DomainBuildingAmendmentResourceVisitor : IAmendmentResourceVisitor<IAmendment>
    {
        public IAmendment Visit(AddComponentAmendmentResource resource)
        {
            return new AddComponentAmendment(resource.TargetPath.ParseComponentPath(), resource.ComponentType.ToComponentName());
        }

        public IAmendment Visit(MoveComponentAmendmentResource resource)
        {
            return new MoveComponentAmendment(
                resource.SourcePath.ParseComponentPath(),
                resource.TargetPath.ParseComponentPath());
        }

        public IAmendment Visit(DeleteComponentAmendmentResource resource)
        {
            return new RemoveComponentAmendment(resource.ComponentPath.ParseComponentPath());
        }

        public IAmendment Visit(SetPropertyAmendmentResource resource)
        {
            return new SetPropertyFixedAmendment(resource.ComponentPath.ParseComponentPath(), resource.PropertyName, resource.Value);
        }
    }
}
