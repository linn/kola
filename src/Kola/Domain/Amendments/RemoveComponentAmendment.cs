﻿namespace Kola.Domain.Amendments
{
    using System;
    using System.Collections.Generic;

    using Kola.Extensions;

    public class RemoveComponentAmendment : IAmendment
    {
        public RemoveComponentAmendment(IEnumerable<int> componentPath)
        {
            this.ComponentPath = componentPath;
        }

        public IEnumerable<int> ComponentPath { get; private set; }

        public void Accept(IAmendmentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IEnumerable<int> GetRootComponent()
        {
            return this.ComponentPath.TakeAllButLast();
        }
    }
}