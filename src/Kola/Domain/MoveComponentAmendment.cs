﻿namespace Kola.Domain
{
    using System.Collections.Generic;

    public class MoveComponentAmendment : Amendment
    {
        public MoveComponentAmendment(IEnumerable<int> parentComponentPath, IEnumerable<int> componentPath, int index)
        {
            this.ParentComponentPath = parentComponentPath;
            this.ComponentPath = componentPath;
            this.Index = index;
        }

        public IEnumerable<int> ParentComponentPath { get; private set; }

        public IEnumerable<int> ComponentPath { get; private set; }

        public int Index { get; private set; }

        public override void Accept(IAmendmentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}