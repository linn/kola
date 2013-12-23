﻿namespace Integration.Tests.Nancy.Modules.RenderingModuleSpecs
{
    using FluentAssertions;

    using global::Nancy;

    using NUnit.Framework;

    public class WhenGettingAPage : ContextBase
    {
        [SetUp]
        public void EstablishContext()
        {
            var path = "/";

            this.Response = this.Browser.Get(path, with => with.Header("Accept", "text/html"));
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnHtml()
        {
            //this.Response.Body.As<this.ShouldReturnHtml()>()
        }
    }
}
