﻿namespace Unit.Tests.Domain.AmendmentApplyingVisitorTests.AddComponent
{
    using System.Linq;

    using FluentAssertions;

    using Kola.Domain;
    using Kola.Domain.Composition;
    using Kola.Domain.Composition.Amendments;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class WhenProcessingAnAddComponentAmendmentToCreateAContainerInAChildComponent : ContextBase
    {
        private Container container;

        [SetUp]
        public void EstablishContext()
        {
            this.container = new Container("existing container", null);
            this.Template.Insert(0, this.container);

            this.ComponentSpecification.Stub(s => s.Create()).Return(new Container("new container", null));

            var amendment = new AddComponentAmendment("new container", new[] { 0, 0 });

            this.Visitor.Visit(amendment);
        }

        [Test]
        public void TheContainerShouldContainOneChild()
        {
            this.container.Components.Should().HaveCount(1);
        }

        [Test]
        public void TheComponentShouldBeAContainer()
        {
            this.container.Components.Single().Should().BeOfType<Container>();
        }
    }
}