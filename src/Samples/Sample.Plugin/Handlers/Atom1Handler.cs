﻿namespace Sample.Plugin.Handlers
{
    using Kola.Domain;
    using Kola.Processing;

    public class Atom1Handler : IHandler
    {
        private readonly IAtom1Dependency atom1Dependency;

        public Atom1Handler(IAtom1Dependency atom1Dependency)
        {
            this.atom1Dependency = atom1Dependency;
        }

        public IRenderingResponse HandleRequest(Component component, RequestContext context)
        {
            return new RenderingResponse(viewHelper => viewHelper.RenderPartial("atom-1", component), null);
        }
    }
}
