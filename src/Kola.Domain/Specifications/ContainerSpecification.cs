﻿namespace Kola.Domain.Specifications
{
    using Kola.Domain.Composition;

    public class ContainerSpecification : PluginComponentSpecification<Container>
    {
        public ContainerSpecification(string name)
            : base(name)
        {
        }

        public override Container Create()
        {
            return new Container(this.Name, this.CreateDefaultProperties());
        }

        public override TV Accept<TV>(IComponentSpecificationVisitor<TV> visitor)
        {
            return visitor.Visit(this);
        }
    }
}