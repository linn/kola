﻿namespace Kola.Persistence.Surrogates.Amendments
{
    public interface IAmendmentSurrogateVisitor<out T>
    {
        T Visit(AddComponentAmendmentSurrogate amendment);

        T Visit(MoveComponentAmendmentSurrogate amendment);

        T Visit(RemoveComponentAmendmentSurrogate amendment);
    }
}