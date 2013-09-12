﻿namespace Integration.Tests.Nancy.Modules.TemplatesModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Kola.Domain;

    using global::Nancy;
    using global::Nancy.Testing;

    using Kola.Resources;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class WhenPostingAComponentToTheTemplateRoot : ContextBase
    {
        [SetUp]
        public void EstablishContext()
        {
            var request = new { };

            var template = new Template(new[] { "test", "path" });
            this.TemplateRepository.Stub(r => r.Get(Arg<IEnumerable<string>>.Is.Anything)).Return(template);

            var component = new Component("component1");
            this.ComponentFactory.Stub(f => f.Create(Arg<string>.Is.Anything)).Return(component);

            this.Response = this.Browser.Post("/_kola/templates/test/path/_components", with => with.JsonBody(request));
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldUpdateTemplate()
        {
            this.TemplateRepository.AssertWasCalled(r => r.Update(Arg<Template>.Is.Anything));
        }

        [Test]
        public void ShouldReturnAResourceWithSelfLink()
        {
            this.Response.Body.DeserializeJson<ComponentResource>().Links.Should().Contain(l => l.Rel == "self");
        }

        [Test]
        public void ShouldReturnSelfLinkWithCorrectHref()
        {
            var link = this.Response.Body.DeserializeJson<ComponentResource>().Links.Where(l => l.Rel == "self").Single();

            link.Href.Should().EndWith("test/path/_components/0");
        }
    }
}
