﻿namespace Unit.Tests.Domain.AmendmentApplyingVisitorTests.AddComponent
{
    using System.Linq;

    using FluentAssertions;

    using Kola.Domain;
    using Kola.Domain.Amendments;
    using Kola.Domain.Templates;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class WhenProcessingAnAddComponentAmendmentToCreateASecondAtomInAChildComponent : ContextBase
    {
        private Container container;

        [SetUp]
        public void EstablishContext()
        {
            this.container = new Container("existing container");
            this.Template.AddComponent(this.container, 0);

            var existingComponent = new Container("existing sub-container");
            this.container.AddComponent(existingComponent, 0);

            this.ComponentSpecification.Stub(s => s.Create()).Return(new Atom("new atom"));

            var amendment = new AddComponentAmendment("atom name", new[] { 0, 1 });

            this.Visitor.Visit(amendment);
        }

        [Test]
        public void TheContainerShouldContainTwoChildren()
        {
            this.container.Components.Should().HaveCount(2);
        }

        [Test]
        public void TheComponentShouldBeAnAtom()
        {
            this.container.Components.ElementAt(1).Should().BeOfType<Atom>();
        }
    }
}