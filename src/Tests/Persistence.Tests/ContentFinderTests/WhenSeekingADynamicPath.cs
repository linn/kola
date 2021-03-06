﻿namespace Persistence.Tests.ContentFinderTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Kola.Domain.DynamicSources;
    using Kola.Domain.Instances.Config;
    using Kola.Persistence;

    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    using Rhino.Mocks;

    public class WhenSeekingADynamicPath : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var source1 = MockRepository.GenerateMock<IDynamicSource>();
            source1.Stub(s => s.Lookup(Arg<string>.Is.Equal("path1"), Arg<IEnumerable<IContextItem>>.Matches(l => !l.Any()))).Return(new DynamicItem("path1", new[] { new ContextItem("item name", "item value") }));

            this.DynamicSourceProvider.Stub(p => p.Get("-dynamic1-")).Return(source1);

            this.FileSystemHelper.Stub(f => f.FindChildDirectories(@"Templates", "-*-")).Return(new[] { "-dynamic1-" });

            this.Result = this.ContentFinder.FindContentDirectories(new[] { "path1" });
        }

        [Test]
        public void OnlyOnePathShouldBeReturned()
        {
            this.Result.Should().HaveCount(1);
        }

        [Test]
        public void TheDirectoryPathShouldBeReturned()
        {
            this.Result.Single().Path.Should().Be(@"Templates\-dynamic1-");
        }

        [Test]
        public void TheContentDirectoryShouldIncludeOneContextItem()
        {
            this.Result.Single().Configuration.ContextItems.Should().HaveCount(1);
        }

        [Test]
        public void TheContextItemShouldHaveTheExpectedNameAndValue()
        {
            this.Result.Single().Configuration.ContextItems.Where(i => i.Name == "item name" && i.Value == "item value").Should().HaveCount(1);
        }
    }
}