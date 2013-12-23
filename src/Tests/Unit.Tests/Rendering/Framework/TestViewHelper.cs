﻿namespace Unit.Tests.Rendering.Framework
{
    using Kola.Processing;

    internal class TestViewHelper : IViewHelper
    {
        private readonly ITestViewFactory viewFactory;

        public TestViewHelper(ITestViewFactory viewFactory)
        {
            this.viewFactory = viewFactory;
        }

        public string RenderPartial<T>(string viewName, T model)
        {
            var view = this.viewFactory.Create(viewName);
            return view.Render(model, this);
        }
    }
}