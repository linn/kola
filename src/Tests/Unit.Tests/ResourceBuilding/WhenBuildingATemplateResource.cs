﻿namespace Unit.Tests.ResourceBuilding
{
    using System.Linq;

    using FluentAssertions;

    using Kola.Domain.Composition;
    using Kola.Domain.Composition.PropertyValues;
    using Kola.ResourceBuilding;
    using Kola.Resources;

    using NUnit.Framework;

    public class WhenBuildingATemplateResource
    {
        private TemplateResource resource;

        [SetUp]
        public void EstablishContext()
        {
            var template = new Template(new[] { "test", "path" }, new IComponent[]
                {
                    new Atom("atom 0", new[]
                        {
                            new Property("property 0", "property 0 type", new FixedPropertyValue("property 0 value"))
                        }), 
                    new Container("container 1", components: new[] { new Atom("atom 1.0") }),
                    new Widget("widget 2", areas: new[] { new Area("area 1", new[] { new Atom("atom 2.0.0") }), })
                });

            this.resource = new TemplateResourceBuilder().Build(template);
        }

        [Test]
        public void ShouldSetCorrectSelfLink()
        {
            this.resource.Links.Single(l => l.Rel == "self").Href.Should().Be("/_kola/templates/test/path");
        }

        [Test]
        public void ShouldBuildAllRootComponents()
        {
            this.resource.Components.Should().HaveCount(3);
        }

        [Test]
        public void ShouldSetCorrectNameForComponent0()
        {
            this.resource.Components.ElementAt(0).As<AtomResource>().Name.Should().Be("atom 0");
        }

        [Test]
        public void ShouldSetCorrectPathForComponent0()
        {
            this.resource.Components.ElementAt(0).As<AtomResource>().Path.Should().Be("/0");
        }

        [Test]
        public void ShouldSetCorrectSelfLinkForComponent0()
        {
            this.resource.Components.ElementAt(0).As<AtomResource>().Links.Single(l => l.Rel == "self").Href.Should().Be("/_kola/templates/test/path/_components/0");
        }

        [Test]
        public void ShouldSetCorrectNameForComponent0Property0()
        {
            this.resource.Components.ElementAt(0).As<AtomResource>().Properties.ElementAt(0).Name.Should().Be("property 0");
        }

        [Test]
        public void ShouldSetCorrectTypeForComponent0Property0()
        {
            this.resource.Components.ElementAt(0).As<AtomResource>().Properties.ElementAt(0).Type.Should().Be("property 0 type");
        }

        [Test]
        public void ShouldSetCorrectValueTypeForComponent0Property0()
        {
            this.resource.Components.ElementAt(0).As<AtomResource>().Properties.ElementAt(0).Value.As<FixedPropertyValueResource>().Type.Should().Be("fixed");
        }

        [Test]
        public void ShouldSetCorrectValueForComponent0Property0()
        {
            this.resource.Components.ElementAt(0).As<AtomResource>().Properties.ElementAt(0).Value.As<FixedPropertyValueResource>().Value.Should().Be("property 0 value");
        }

        [Test]
        public void ShouldSetCorrectNameForComponent1()
        {
            this.resource.Components.ElementAt(1).As<ContainerResource>().Name.Should().Be("container 1");
        }

        [Test]
        public void ShouldSetCorrectPathForComponent1()
        {
            this.resource.Components.ElementAt(1).As<ContainerResource>().Path.Should().Be("/1");
        }

        [Test]
        public void ShouldSetCorrectSelfLinkForComponent1()
        {
            this.resource.Components.ElementAt(1).As<ContainerResource>().Links.Single(l => l.Rel == "self").Href.Should().Be("/_kola/templates/test/path/_components/1");
        }

        [Test]
        public void ShouldSetCorrectNameForComponent10()
        {
            this.resource.Components.ElementAt(1).As<ContainerResource>().Components.ElementAt(0).As<AtomResource>().Name.Should().Be("atom 1.0");
        }

        [Test]
        public void ShouldSetCorrectPathForComponent10()
        {
            this.resource.Components.ElementAt(1).As<ContainerResource>().Components.ElementAt(0).As<AtomResource>().Path.Should().Be("/1/0");
        }

        [Test]
        public void ShouldSetCorrectSelfLinkForComponent10()
        {
            this.resource.Components.ElementAt(1).As<ContainerResource>().Components.ElementAt(0).As<AtomResource>().Links.Single(l => l.Rel == "self").Href.Should().Be("/_kola/templates/test/path/_components/1/0");
        }


        [Test]
        public void ShouldSetCorrectNameForComponent2()
        {
            this.resource.Components.ElementAt(2).As<WidgetResource>().Name.Should().Be("widget 2");
        }

        [Test]
        public void ShouldSetCorrectPathForComponent2()
        {
            this.resource.Components.ElementAt(2).As<WidgetResource>().Path.Should().Be("/2");
        }

        [Test]
        public void ShouldSetCorrectSelfLinkForComponent2()
        {
            this.resource.Components.ElementAt(2).As<WidgetResource>().Links.Single(l => l.Rel == "self").Href.Should().Be("/_kola/templates/test/path/_components/2");
        }

        [Test]
        public void ShouldSetCorrectNameForComponent200()
        {
            this.resource.Components.ElementAt(2).As<WidgetResource>().Areas.ElementAt(0).As<AreaResource>().Components.ElementAt(0).As<AtomResource>().Name.Should().Be("atom 2.0.0");
        }

        [Test]
        public void ShouldSetCorrectPathForComponent200()
        {
            this.resource.Components.ElementAt(2).As<WidgetResource>().Areas.ElementAt(0).As<AreaResource>().Components.ElementAt(0).As<AtomResource>().Path.Should().Be("/2/0/0");
        }

        [Test]
        public void ShouldSetCorrectSelfLinkForComponent200()
        {
            this.resource.Components.ElementAt(2).As<WidgetResource>().Areas.ElementAt(0).As<AreaResource>().Components.ElementAt(0).As<AtomResource>().Links.Single(l => l.Rel == "self").Href.Should().Be("/_kola/templates/test/path/_components/2/0/0");
        }

    }
}