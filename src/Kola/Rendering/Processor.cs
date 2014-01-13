﻿namespace Kola.Rendering
{
    using Kola.Domain;

    public class Processor : IProcessor
    {
        private readonly IHandlerFactory handlerFactory;

        public Processor(IHandlerFactory handlerFactory)
        {
            this.handlerFactory = handlerFactory;
        }

        public IResult Process(IComponentInstance component)
        {
            var handler = this.handlerFactory.Create(component);
            return handler.HandleRequest(component);
        }
    }
}