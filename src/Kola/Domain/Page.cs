﻿namespace Kola.Domain
{
    // TODO {SC} This is wrong - the components in a composites are definitions, not evaluated instances
    public class Page : CompositeComponent
    {
        public string Title { get; set; }
    }
}