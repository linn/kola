﻿namespace Kola.Domain.Amendments
{
    using System.Collections.Generic;

    using Kola.Domain;

    public class AddComponentAmendment : IAmendment
    {
        public AddComponentAmendment(string componentName, IEnumerable<int> targetPath)
        {
            this.ComponentName = componentName;
            this.TargetPath = targetPath;
        }

        public string ComponentName { get; private set; }

        public IEnumerable<int> TargetPath { get; internal set; }

        public IEnumerable<int> GetRootComponent()
        {
            return this.TargetPath;
        }

        public void Accept(IAmendmentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}