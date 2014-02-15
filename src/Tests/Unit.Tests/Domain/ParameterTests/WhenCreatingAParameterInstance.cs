﻿namespace Unit.Tests.Domain.ParameterTests
{
    using FluentAssertions;

    using Kola.Domain;
    using Kola.Domain.Instances;
    using Kola.Domain.Composition;
    using Kola.Domain.Composition.ParameterValues;

    using NUnit.Framework;

    public class WhenCreatingAnInstanceOfAParameterWithAFixedParameterValue
    {
        private ParameterInstance parameterInstance;

        [SetUp]
        public void EstablishContext()
        {
            var parameter = new Parameter("parameter name", "parameter type")
                {
                    Value = new FixedParameterValue("parameter value")
                };

            this.parameterInstance = parameter.Build(null);
        }

        [Test]
        public void TheParameterInstanceShouldNotBeNull()
        {
            this.parameterInstance.Should().NotBeNull();
        }

        [Test]
        public void TheParameterInstanceShouldHaveTheCorrectName()
        {
            this.parameterInstance.Name.Should().Be("parameter name");
        }

        [Test]
        public void TheParameterInstanceShouldHaveTheCorrectValue()
        {
            this.parameterInstance.Value.Should().Be("parameter value");
        }
    }
}