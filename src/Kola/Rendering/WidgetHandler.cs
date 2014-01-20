﻿namespace Kola.Rendering
{
    using Kola.Domain;
    using Kola.Domain.Instances;

    public class WidgetHandler : IHandler
    {
        private readonly IKolaEngine kolaEngine;

        public WidgetHandler(IKolaEngine kolaEngine)
        {
            this.kolaEngine = kolaEngine;
        }

        public IResult HandleRequest(IComponentInstance component)
        {
            var widgetInstance = (WidgetInstance)component;

            return this.kolaEngine.Render(widgetInstance.Children);
        }
    }
}