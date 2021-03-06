﻿namespace Kola.Domain.Composition.Amendments
{
    using System.Collections.Generic;

    using Kola.Domain.Extensions;

    public class AddComponentAmendment : IAmendment
    {
        public AddComponentAmendment(IEnumerable<int> targetPath, string componentName)
        {
            this.ComponentName = componentName;
            this.TargetPath = targetPath;
        }

        public string ComponentName { get; private set; }

        public IEnumerable<int> TargetPath { get; internal set; }

        public IEnumerable<IEnumerable<int>> AffectedPaths
        {
            get
            {
                yield return this.TargetPath.TakeAllButLast();
            }
        }

        public IEnumerable<int> SubjectPath => this.TargetPath;

        public void Accept(IAmendmentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public T Accept<T>(IAmendmentVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public T Accept<T, TContext>(IAmendmentVisitor<T, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }
}