﻿//namespace Unit.Tests.Extensions.EnumerableExtensions.Overlap
//{
//    using System.Collections.Generic;

//    using FluentAssertions;

//    using Kola.Service.Extensions;

//    using NUnit.Framework;

//    public class WhenFindingOverlapOfNonEmptyLists
//    {
//        private IEnumerable<int> result;

//        [SetUp]
//        public void SetUp()
//        {
//            var list1 = new[] { 1, 2, 3, 4, 5 };
//            var list2 = new[] { 1, 2, 3 };

//            this.result = list1.GetOverlap(list2);
//        }

//        [Test]
//        public void ShouldReturnCommonElements()
//        {
//            this.result.Should().ContainInOrder(new[] { 1, 2, 3 });
//        }
//    }
//}