﻿namespace Kola.Persistence.SurrogateBuilding
{
    using System;

    using Kola.Domain.Composition.Amendments;
    using Kola.Persistence.Extensions;
    using Kola.Persistence.Surrogates.Amendments;

    internal class SurrogateBuildingAmendmentVisitor : IAmendmentVisitor<AmendmentSurrogate>
    {
        public AmendmentSurrogate Visit(AddComponentAmendment amendment)
        {
            return new AddComponentAmendmentSurrogate
            {
                TargetPath = amendment.TargetPath.ToComponentPathString(),
                ComponentType = amendment.ComponentName,
            };
        }

        public AmendmentSurrogate Visit(MoveComponentAmendment amendment)
        {
            return new MoveComponentAmendmentSurrogate
            {
                SourcePath = amendment.SourcePath.ToComponentPathString(),
                TargetPath = amendment.TargetPath.ToComponentPathString()
            };
        }

        public AmendmentSurrogate Visit(RemoveComponentAmendment amendment)
        {
            return new RemoveComponentAmendmentSurrogate
            {
                ComponentPath = amendment.ComponentPath.ToComponentPathString(),
            };
        }

        public AmendmentSurrogate Visit(DuplicateComponentAmendment amendment)
        {
            return new DuplicateComponentAmendmentSurrogate
            {
                ComponentPath = amendment.ComponentPath.ToComponentPathString(),
            };
        }

        public AmendmentSurrogate Visit(SetPropertyFixedAmendment amendment)
        {
            return new SetPropertyFixedAmendmentSurrogate
            {
                ComponentPath = amendment.ComponentPath.ToComponentPathString(),
                PropertyName = amendment.PropertyName,
                FixedValue = amendment.FixedValue
            };
        }

        public AmendmentSurrogate Visit(SetPropertyInheritedAmendment amendment)
        {
            throw new NotImplementedException();
        }

        public AmendmentSurrogate Visit(SetPropertyMultilingualAmendment amendment)
        {
            throw new NotImplementedException();
        }
    }
}