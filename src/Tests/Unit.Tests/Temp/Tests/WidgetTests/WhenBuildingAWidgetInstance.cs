﻿namespace Unit.Tests.Temp.Tests.WidgetTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using NUnit.Framework;

    using Rhino.Mocks;

    using Unit.Tests.Temp.Domain;
    using Unit.Tests.Temp.Domain.Instances;
    using Unit.Tests.Temp.Domain.Specifications;
    using Unit.Tests.Temp.Domain.Templates;

    public class WhenBuildingAWidgetInstance
    {
        private WidgetInstance instance;

        [SetUp]
        public void EstablishContext()
        {
            var specification = new WidgetSpecification(
                "widget name",
                new IComponent[]
                    {
                        new AtomTemplate(), 
                        new Placeholder(), 
                        new ContainerTemplate(new[] { new Placeholder() }) 
                    });

            var widget = new WidgetTemplate(
                "widget name",
                new[]
                    {
                        new Area(new IComponent[] { new AtomTemplate(), new AtomTemplate() }),
                        new Area(new IComponent[] { new AtomTemplate(), new AtomTemplate(), new AtomTemplate() })
                    });

            var buildContext = MockRepository.GenerateStub<IBuildContext>();
            var widgetStack = new Stack<Queue<Area>>();
            buildContext.Stub(c => c.WidgetSpecificationLocator).Return(n => specification);
            buildContext.Stub(c => c.Areas).Return(widgetStack);

            this.instance = (WidgetInstance)widget.Build(buildContext);
        }

        [Test]
        public void WidgetInstanceShouldHaveFourChildren()
        {
            this.instance.Components.Should().HaveCount(3);
        }

        [Test]
        public void FirstPlaceholderInstanceShouldHaveTwoChildren()
        {
            var placeholder = this.instance.Components.ElementAt(1) as PlaceholderInstance;
            placeholder.Components.Should().HaveCount(2);
        }

        [Test]
        public void ContainerIntanceShouldContainPlaceholderInstance()
        {
            var containerInstance = (ContainerInstance)this.instance.Components.ElementAt(2);

            containerInstance.Children.Single().Should().BeOfType<PlaceholderInstance>();
        }

        [Test]
        public void SecondPlaceholderInstanceShouldHaveThreeChildren()
        {
            var containerInstance = (ContainerInstance)this.instance.Components.ElementAt(2);
            var placeholder = containerInstance.Children.Single() as PlaceholderInstance;
            placeholder.Components.Should().HaveCount(3);
        }
    }
}