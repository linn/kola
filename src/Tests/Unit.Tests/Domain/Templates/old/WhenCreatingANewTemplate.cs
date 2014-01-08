﻿namespace Unit.Tests.Domain.Templates
{
    using Kola.Domain;

    using NUnit.Framework;

    public class WhenCreatingANewTemplate
    {
        private Template template;

        [SetUp]
        public void EstablishContext()
        {
            var templatePath = new[] { "test", "path" };
            this.template = new Template(templatePath);
        }
    }
}
