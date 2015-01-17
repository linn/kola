﻿namespace Kola.Resources
{
    public interface IAmendmentResourceVisitor<out T>
    {
        T Visit(AddComponentAmendmentResource resource);

        T Visit(MoveComponentAmendmentResource resource);

        T Visit(RemoveComponentAmendmentResource resource);

        T Visit(SetPropertyAmendmentResource resource);
    }
}