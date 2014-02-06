﻿namespace Unit.Tests.Domain.WidgetTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Kola.Domain.Instances;
    using Kola.Domain.Instances.Building;
    using Kola.Domain.Specifications;
    using Kola.Domain.Templates;

    using NUnit.Framework;

    using Rhino.Mocks;

    public class WhenBuildingANestedWidgetWithEmbeddedPlaceholderInstance
    {
        private WidgetInstance instance;

        [SetUp]
        /*
         * == given specifications ==
         * Widget specification 1
         * ├─ Atom A
         * ├─ Placeholder
         * └─ Container A
         *    └─ Placeholder
         * 
         * Widget specification 2
         * ├─ Placeholder
         * └─ Widget (widget specification 1)
         *    ├─ Area
         *    │  └─ Placeholder
         *    └─ Area
         *       └─ Atom B
         *
         * == and widget template ==
         * Widget (widget specification 2)
         * ├─ Area
         * │  └─ Atom C
         * └─ Area
         *    └─ Atom D
         * 
         * == should build to ==
         * WidgetInstance (widget specification 2)      0
         * ├─ PlaceholderInstance                       0.0
         * │  └─ AtomInstance C                         0.0.0
         * └─ WidgetInstance (widget specification 1)   1
         *    ├─ AtomInstance A                         1.0
         *    ├─ PlaceholderInstance                    1.1
         *    │  └─ PlaceholderInstance                 1.1.0
         *    │    └─ AtomInstance D                    1.1.0.0
         *    └─ ContainerInstance A                    1.2
         *       └─ PlaceholderInstance                 1.2.0
         *          └─ AtomInstance B                   1.2.0.0
         */
        public void EstablishContext()
        {
            var specification1 = new WidgetSpecification(
                "widget 1",
                new IComponentTemplate[]
                    {
                        new AtomTemplate("atom a", Enumerable.Empty<ParameterTemplate>()), 
                        new PlaceholderTemplate(), 
                        new ContainerTemplate(
                            "container a",
                            Enumerable.Empty<ParameterTemplate>(),
                            new[]
                                {
                                    new PlaceholderTemplate()
                                }) 
                    });

            var specification2 = new WidgetSpecification(
                "widget 2",
                new IComponentTemplate[]
                    {
                        new PlaceholderTemplate(), 
                        new WidgetTemplate(
                            "widget 1", 
                            Enumerable.Empty<ParameterTemplate>(),
                            new[]
                                {
                                    new Area(new[]
                                        {
                                            new PlaceholderTemplate()
                                        }),
                                    new Area(new[]
                                        {
                                            new AtomTemplate("atom b", Enumerable.Empty<ParameterTemplate>())
                                        })
                                })
                    });

            var widget = new WidgetTemplate(
                "widget 2",
                Enumerable.Empty<ParameterTemplate>(),
                new[]
                    {
                        new Area(
                            new IComponentTemplate[]
                                {
                                    new AtomTemplate("atom c", Enumerable.Empty<ParameterTemplate>())
                                }),
                        new Area(
                            new IComponentTemplate[]
                                {
                                    new AtomTemplate("atom d", Enumerable.Empty<ParameterTemplate>()),
                                })
                    });

            var buildContext = MockRepository.GenerateStub<IBuildContext>();
            buildContext.Stub(c => c.WidgetSpecificationFinder).Return(n => n == "widget 1" ? specification1 : specification2);
            buildContext.Stub(c => c.Areas).Return(new Stack<Queue<IEnumerable<IComponentInstance>>>());

            this.instance = (WidgetInstance)widget.Build(buildContext);
        }

        [Test]
        public void WidgetInstanceShouldHaveTwoComponents()
        {
            this.instance.Components.Should().HaveCount(2);
        }

        [Test]
        public void Component_0_ShouldBeAPlaceholderInstance()
        {
            this.instance.Components.ElementAt(0).Should().BeOfType<PlaceholderInstance>();
        }

        [Test]
        public void Component_1_ShouldBeAWidgetInstance()
        {
            this.instance.Components.ElementAt(1).Should().BeOfType<WidgetInstance>();
        }

        [Test]
        public void Component_0_ShouldHaveOneComponent()
        {
            var placeholder = this.instance.Components.ElementAt(0) as PlaceholderInstance;
            placeholder.Components.Should().HaveCount(1);
        }

        [Test]
        public void Component_0_0_ShouldBeAnAtomInstance()
        {
            var placeholder = this.instance.Components.ElementAt(0) as PlaceholderInstance;
            placeholder.Components.ElementAt(0).Should().BeOfType<AtomInstance>();
        }

        [Test]
        public void Component_0_0_ShouldBeAtomInstanceC()
        {
            var placeholder = this.instance.Components.ElementAt(0) as PlaceholderInstance;
            var atom = placeholder.Components.ElementAt(0) as AtomInstance;
            atom.Name.Should().Be("atom c");
        }

        [Test]
        public void Component_1_ShouldHaveThreeComponents()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            widget.Components.Should().HaveCount(3);
        }

        [Test]
        public void Component_1_0_ShouldBeAnAtomInstance()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            widget.Components.ElementAt(0).Should().BeOfType<AtomInstance>();
        }

        [Test]
        public void Component_1_0_ShouldBeAtomInstanceA()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var atom = widget.Components.ElementAt(0) as AtomInstance;
            atom.Name.Should().Be("atom a");
        }

        [Test]
        public void Component_1_1_ShouldBeAPlaceholderInstance()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            widget.Components.ElementAt(1).Should().BeOfType<PlaceholderInstance>();
        }

        [Test]
        public void Component_1_2_ShouldBeAContainerInstance()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            widget.Components.ElementAt(2).Should().BeOfType<ContainerInstance>();
        }

        [Test]
        public void Component_1_1_ShouldHaveOneComponent()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var placeholder = widget.Components.ElementAt(1) as PlaceholderInstance;
            placeholder.Components.Should().HaveCount(1);
        }

        [Test]
        public void Component_1_1_0_ShouldBeAPlaceholderInstance()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var placeholder = widget.Components.ElementAt(1) as PlaceholderInstance;
            placeholder.Components.ElementAt(0).Should().BeOfType<PlaceholderInstance>();
        }

        [Test]
        public void Component_1_1_0_ShouldHaveOneComponent()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var placeholder = widget.Components.ElementAt(1) as PlaceholderInstance;
            var placeholder2 = placeholder.Components.ElementAt(0) as PlaceholderInstance;
            placeholder2.Components.Should().HaveCount(1);
        }

        [Test]
        public void Component_1_1_0_0_ShouldBeAnAtomInstance()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var placeholder = widget.Components.ElementAt(1) as PlaceholderInstance;
            var placeholder2 = placeholder.Components.ElementAt(0) as PlaceholderInstance;
            placeholder2.Components.ElementAt(0).Should().BeOfType<AtomInstance>();
        }

        [Test]
        public void Component_1_1_0_0_ShouldBeAtomInstanceD()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var placeholder = widget.Components.ElementAt(1) as PlaceholderInstance;
            var placeholder2 = placeholder.Components.ElementAt(0) as PlaceholderInstance;
            var atom = placeholder2.Components.ElementAt(0) as AtomInstance;
            atom.Name.Should().Be("atom d");
        }

        [Test]
        public void Component_1_2_ShouldHaveOneComponent()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var container = widget.Components.ElementAt(2) as ContainerInstance;
            container.Components.Should().HaveCount(1);
        }

        [Test]
        public void Component_1_2_0_ShouldBeAPlaceholderInstance()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var container = widget.Components.ElementAt(2) as ContainerInstance;
            container.Components.ElementAt(0).Should().BeOfType<PlaceholderInstance>();
        }

        [Test]
        public void Component_1_2_0_ShouldHaveOneComponent()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var container = widget.Components.ElementAt(2) as ContainerInstance;
            var placeholder = container.Components.ElementAt(0) as PlaceholderInstance;
            placeholder.Components.Should().HaveCount(1);
        }

        [Test]
        public void Component_1_2_0_0_ShouldBeAnAtomInstance()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var container = widget.Components.ElementAt(2) as ContainerInstance;
            var placeholder = container.Components.ElementAt(0) as PlaceholderInstance;
            placeholder.Components.ElementAt(0).Should().BeOfType<AtomInstance>();
        }

        [Test]
        public void Component_1_2_0_0_ShouldBeAtomInstanceB()
        {
            var widget = this.instance.Components.ElementAt(1) as WidgetInstance;
            var container = widget.Components.ElementAt(2) as ContainerInstance;
            var placeholder = container.Components.ElementAt(0) as PlaceholderInstance;
            var atom = placeholder.Components.ElementAt(0) as AtomInstance;
            atom.Name.Should().Be("atom b");
        }
    }
}