﻿namespace Kola.Domain.Templates
{
    public class Widget : IComponent
    {
        public Widget(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public void Accept(IComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}