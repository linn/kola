﻿namespace Kola.Rendering
{
    public class DefaultHandler : IHandler
    {
        public IResult HandleRequest(IComponent component)
        {
            return new Result(h => h.RenderPartial(component.Name, component));
        }
    }
}