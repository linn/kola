﻿namespace Unit.Tests.Domain.TemplateTests
{
    using Kola.Domain;
    using Kola.Domain.Templates;

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