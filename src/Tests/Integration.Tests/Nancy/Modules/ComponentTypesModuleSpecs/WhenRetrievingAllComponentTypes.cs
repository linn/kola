﻿namespace Integration.Tests.Nancy.Modules.ComponentTypesModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using global::Nancy;
    using global::Nancy.Testing;

    using Kola.Domain.Specifications;
    using Kola.Resources;

    using NUnit.Framework;

    using Rhino.Mocks;

    internal class WhenRetrievingAllComponentTypes : ContextBase
    {
        [SetUp]
        public void EstablishContext()
        {
            var components = new[]
                {
                    new ContainerSpecification("Component A"), 
                    new ContainerSpecification("Component B") 
                };

            this.ComponentLibrary.Stub(r => r.FindAll()).Return(components);
            this.Response = this.Browser.Get("/_kola/component-types", b => b.Header("Accept", "application/json"));
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnTwoComponentTypeResources()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ComponentTypeResource>>();
            resources.Should().HaveCount(2);
        }

        [Test]
        public void EachResourceShouldHaveASelfLink()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ComponentTypeResource>>();

            foreach (var resource in resources)
            {
                resource.Links.Should().Contain(l => l.Rel == "self");
            }
        }

        [Test]
        public void FirstResourceShouldHaveValidHrefValue()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ComponentTypeResource>>();

            resources.First().Links.Single(l => l.Rel == "self").Href.Should().Be("/_kola/component-types/component-a");
        }

        [Test]
        public void SecondResourceShouldHaveValidHrefValue()
        {
            var resources = this.Response.Body.DeserializeJson<IEnumerable<ComponentTypeResource>>();

            resources.ElementAt(1).Links.Single(l => l.Rel == "self").Href.Should().Be("/_kola/component-types/component-b");
        }
    }
}
