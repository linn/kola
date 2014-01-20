﻿namespace Unit.Tests.Domain.AmendmentApplyingVisitorTests.AddComponent
{
    using System.Linq;

    using FluentAssertions;

    using Kola.Domain;
    using Kola.Domain.Amendments;
    using Kola.Domain.Templates;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class WhenProcessingAnAddComponentAmendmentToCreateAContainerInTheRoot : ContextBase
    {
        [SetUp]
        public void EstablishContext()
        {
            this.ComponentSpecification.Stub(s => s.Create()).Return(new Container("new container"));

            var amendment = new AddComponentAmendment("new container", new[] { 0 });

            this.Visitor.Visit(amendment);
        }

        [Test]
        public void TheTemplateShouldContainOneComponent()
        {
            this.Template.Components.Should().HaveCount(1);
        }

        [Test]
        public void TheComponentShouldBeAContainer()
        {
            this.Template.Components.Single().Should().BeOfType<Container>();
        }
    }
}