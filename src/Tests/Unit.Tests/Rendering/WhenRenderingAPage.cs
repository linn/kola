﻿namespace Unit.Tests.Rendering
{
    using System.Linq;

    using FluentAssertions;

    using Kola.Domain.Instances;
    using Kola.Rendering;

    using NUnit.Framework;

    using Rhino.Mocks;

    using Unit.Tests.Rendering.Framework;

    public class WhenRenderingAPage
    {
        private string result;

        [SetUp]
        public void EstablishContext()
        {
            var handlerFactory = MockRepository.GenerateStub<IHandlerFactory>();
            handlerFactory.Stub(h => h.GetAtomHandler(Arg<string>.Is.Anything)).Return(new DefaultHandler());
            handlerFactory.Stub(h => h.GetContainerHandler(Arg<string>.Is.Anything)).Return(new DefaultHandler());

            var processor = new Processor(handlerFactory);
            var engine = new KolaEngine(processor);
            var page =
                new PageInstance(
                    new IComponentInstance[]
                        {
                            new AtomInstance("atom1", Enumerable.Empty<ParameterInstance>()), 
                            new AtomInstance("atom2", Enumerable.Empty<ParameterInstance>()),
                            new ContainerInstance("container1", null, new[] { new AtomInstance("atom3", Enumerable.Empty<ParameterInstance>()) })
                        });

            var viewFactory = new TestViewFactory(engine);
            var viewHelper = new TestViewHelper(viewFactory);


            var renderedPage = engine.Render(page);
            this.result = renderedPage.ToHtml(viewHelper);
        }

        [Test]
        public void ShouldReturnAResult()
        {
            this.result.Should().NotBeNull();
        }

        [Test]
        public void ResultShouldIncludeHtmlForAtom1()
        {
            this.result.Should().Contain("<atom1/>");
        }

        [Test]
        public void ResultShouldIncludeHtmlForAtom2()
        {
            this.result.Should().Contain("<atom2/>");
        }

        [Test]
        public void ResultShouldIncludeHtmlForStartOfContainer1()
        {
            this.result.Should().Contain("<container1>");
        }

        [Test]
        public void ResultShouldIncludeHtmlForEndOfContainer1()
        {
            this.result.Should().Contain("</container1>");
        }

        [Test]
        public void ResultShouldIncludeHtmlForAtom3()
        {
            this.result.Should().Contain("<atom3/>");
        }
    }
}