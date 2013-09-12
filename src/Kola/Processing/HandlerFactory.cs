﻿using System;
using System.Collections.Generic;
using Kola.Domain;

namespace Kola.Processing
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly IDictionary<string, Type> handlerMappings;
        private readonly IObjectFactory objectFactory;

        public HandlerFactory(IDictionary<string, Type> handlerMappings, IObjectFactory objectFactory)
        {
            this.handlerMappings = handlerMappings;
            this.objectFactory = objectFactory;
        }

        public IHandler GetHandler(Component component)
        {
            if (this.handlerMappings.ContainsKey(component.Name))
            {
                var handlerType = handlerMappings[component.Name];
                return this.objectFactory.Resolve<IHandler>(handlerType);
            }

            throw new Exception("No handler found for component '" + component.Name + "'");
        }
    }

    public interface IObjectFactory
    {
        T Resolve<T>(Type type);
    }
}