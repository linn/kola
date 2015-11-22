﻿namespace Kola.Persistence
{
    using System.Collections.Generic;

    using Kola.Domain.Composition;
    using Kola.Domain.Instances.Context;

    public class FindContentResult
    {
        public FindContentResult(IContent content, IEnumerable<IContextItem> context)
        {
            this.Content = content;
            this.Context = context;
        }

        public IContent Content { get; }

        public IEnumerable<IContextItem> Context { get; }
    }
}