﻿using System;
using Kola.Domain;

namespace Kola.Processing
{
    internal class ComponentRenderer : IComponentRenderer
    {
        public IRenderingResponse RenderComponent(Component component, RequestContext context)
        {
            var handler = context.HandlerFactory.GetHandler(component);

            return handler.HandleRequest(component, context);
        }
    }
}