﻿using NUnit.Framework;
using VkApiGenerator.Model;
using FluentNUnit;

namespace VkApiGenerator.Test.Model
{
    [TestFixture]
    public class VkMethodParamsCollectionTest
    {
        [Test]
        public void ToString_EmptyList()
        {
            var collection = new VkMethodParamsCollection();
            string result = collection.ToString();
            result.ShouldEqual(string.Empty);
        }

        [Test]
        public void ToString_OneItem()
        {
            var collection = new VkMethodParamsCollection {new VkMethodParam {Name = "count", Type = ReturnType.Long}};

            string result = collection.ToString();
            result.ShouldEqual("int? count = null");
        }

        [Test]
        public void ToString_TwoItems()
        {
            var collection = new VkMethodParamsCollection
            {
                new VkMethodParam {Name = "count", Type = ReturnType.Long},
                new VkMethodParam {Name = "offset", Type = ReturnType.Long}
            };

            string result = collection.ToString();
            result.ShouldEqual("int? count = null, int? offset = null");
        }
    }
}