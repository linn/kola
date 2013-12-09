﻿using Kola.Domain;

namespace Kola.Processing
{
    public class CachingHandlerFactory : IHandlerFactory
    {
        private readonly IHandlerFactory innerHandlerFactory;

        public CachingHandlerFactory(IHandlerFactory innerHandlerFactory)
        {
            this.innerHandlerFactory = innerHandlerFactory;
        }

        public IHandler GetHandler(Component component)
        {
            return this.Retrieve(component)
                   ?? this.Cache(this.innerHandlerFactory.GetHandler(component));
        }

        private IHandler Cache(IHandler handler)
        {
            //Add to cache...

            return handler;
        }

        private IHandler Retrieve(Component component)
        {
            //Lookup cache

            return null;
        }
    }
}