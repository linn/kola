﻿namespace Integration.Tests.Nancy.Modules.RenderingModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Integration.Tests.Nancy.Modules.RenderingModuleSpecs.Framework;

    using Kola.Domain;
    using Kola.Rendering;

    using global::Nancy;
    using global::Nancy.Testing;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class WhenGettingAPage : ContextBase
    {
        [SetUp]
        public void EstablishContext()
        {
            var page = new Page(new[] { new AtomInstance("atom1") });

            var atom1Handler = MockRepository.GenerateMock<IHandler>();
            atom1Handler.Stub(h => h.HandleRequest(Arg<IComponentInstance>.Is.Anything)).Return(new Result(h => "<atom1/>"));

            this.PageHandler.Stub(h => h.GetPage(Arg<IEnumerable<string>>.Is.Anything)).Return(page);
            this.HandlerFactory.Stub(f => f.Create(Arg<IComponentInstance>.Is.Anything)).Return(atom1Handler);

            this.Response = this.Browser.Get("/", with => with.Header("Accept", "text/html"));
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnHtml()
        {
            this.Response.Body.AsString().Should().Contain("<atom1/>");
        }
    }
}
