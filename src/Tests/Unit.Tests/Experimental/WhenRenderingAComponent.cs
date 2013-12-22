﻿//namespace Unit.Tests.Experimental
//{
//    using FluentAssertions;

//    using Kola.Experimental;

//    using NUnit.Framework;

//    using Rhino.Mocks;

//    public class WhenRenderingAComponent
//    {
//        private IKolaResponse result;

//        [SetUp]
//        public void EstablishContext()
//        {
//            var handler = new DefaultHandler();

//            var component = MockRepository.GenerateStub<IKolaComponent>();
//            var viewHelper = MockRepository.GenerateStub<IViewHelper>();
//            var response = MockRepository.GenerateStub<IKolaResponse>();

//            component.Stub(c => c.Name).Return("component name");
//            viewHelper.Stub(v => v.RenderPartial("component name", component)).Return(response);
//            response.Stub(r => r.ToHtml()).Return("response body");

//            this.result = handler.Render(component, viewHelper);
//        }

//        [Test]
//        public void ShouldReturnSomething()
//        {
//            this.result.ToHtml().Should().NotBeNullOrEmpty();
//        }

//        [Test]
//        public void ShouldReturnHtml()
//        {
//            this.result.ToHtml().Should().Contain("response body");
//        }
//    }
//}