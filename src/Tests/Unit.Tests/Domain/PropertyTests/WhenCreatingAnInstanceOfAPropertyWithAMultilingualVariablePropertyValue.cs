﻿namespace Unit.Tests.Domain.PropertyTests
{
    using FluentAssertions;

    using Kola.Domain.Composition;
    using Kola.Domain.Composition.PropertyValues;
    using Kola.Domain.Instances;
    using Kola.Domain.Instances.Building;

    using NUnit.Framework;

    public class WhenCreatingAnInstanceOfAPropertyWithAMultilingualVariablePropertyValue
    {
        private PropertyInstance propertyInstance;

        [SetUp]
        public void EstablishContext()
        {
            var property = new Property(
                "property name",
                "property type",
                new MultilingualPropertyValue(
                    new[]
                        {
                            new MultilingualVariant("en", "English"), 
                            new MultilingualVariant("fr", "Français")
                        }));

            var context = new Context { LanguageCode = "fr" };

            var buildContext = new BuildContext();
            buildContext.PushContext(context);
            this.propertyInstance = property.Build(buildContext);
        }

        [Test]
        public void ThePropertyInstanceShouldNotBeNull()
        {
            this.propertyInstance.Should().NotBeNull();
        }

        [Test]
        public void ThePropertyInstanceShouldHaveTheCorrectName()
        {
            this.propertyInstance.Name.Should().Be("property name");
        }

        [Test]
        public void ThePropertyInstanceShouldHaveTheCorrectValue()
        {
            this.propertyInstance.Value.Should().Be("Français");
        }
    }
}