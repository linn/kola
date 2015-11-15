﻿namespace Integration.Tests.Nancy.Modules.TemplatesModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;

    using Kola.Domain.Composition;
    using Kola.Domain.Specifications;

    using global::Nancy;
    using global::Nancy.Testing;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class WhenPostingAnAmendment : ContextBase
    {
        private Template template;

        [SetUp]
        public void EstablishContext()
        {
            var templatePath = @"test/path";

            this.template = new Template(new[] { "test", "path" });

            var component = MockRepository.GenerateStub<IComponentWithProperties>();
            var componentSpecification =
                MockRepository.GenerateStub<IComponentSpecification<IComponentWithProperties>>();

            componentSpecification.Stub(s => s.Create()).Return(component);
            this.ContentRepository.Stub(r => r.Get(Arg<IEnumerable<string>>.Is.Anything)).Return(this.template);
            this.ComponentLibrary.Stub(r => r.Lookup("component name")).Return(componentSpecification);

            var request = new { targetPath = "0", componentType = "component name" };

            this.Response =
                this.Browser.Post(
                    string.Format("/_kola/templates/{0}/_amendments/addComponent", templatePath),
                    with =>
                        {
                            with.JsonBody(request);
                            with.Header("Accept", "application/json");
                        });
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldUpdateTemplateToRepository()
        {
            this.ContentRepository.AssertWasCalled(r => r.Update(Arg<Template>.Is.Anything));
        }

        [Test]
        public void ShouldAddAnAmendmentToTheTemplate()
        {
            var args = this.ContentRepository.GetArgumentsForCallsMadeOn(r => r.Update(Arg<Template>.Is.Anything));
            var updatedTemplate = (Template)args[0][0];
            updatedTemplate.Amendments.Should().HaveCount(1);
        }
    }
}
