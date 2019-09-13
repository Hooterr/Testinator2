using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Exceptions;
using Xunit;

namespace Testinator.TestSystem.Implementation.Test.Attributes
{
    public class AttributeHelperTests
    {
        public static Dictionary<int, int> VersionMaxCount = new Dictionary<int, int>()
        {
            { 1, 1 },
            { 2, 2 },
            { 3, 2 },
            { 4, 2 },
            { 5, 9 },
            { 6, 9 },
            { 7, 9 },
            { 8, 3 },
            { 9, 3 }
        };

        public static IEnumerable<object[]> Range(int from, int count)
        {
            return Enumerable.Range(from, count).Select(x => new object[] { x });
        }

        [Theory]
        [MemberData(nameof(Range), new object[] { 1, 9 })]
        public void VersioningTestAllGood(int version)
        {
            var value = AttributeHelper.GetPropertyAttributeValue<TestClassWithAttributes, MaxCollectionCountAttribute, int>
                (x => x.PropAllGood, attr => attr.MaxCount, version);
            Assert.Equal(VersionMaxCount[version], value);
        }

        [Fact]
        public void VersioningTestAmbiguityAtPassedVersion()
        {
            Assert.Throws<VersioningAmbiguityException>(() => AttributeHelper.GetPropertyAttributeValue<TestClassWithAttributes, MaxCollectionCountAttribute, int>
                (x => x.PropAmbiguityFromVersion3, attr => attr.MaxCount, 3));
        }

        [Fact]
        public void VersioningTestAmbiguityAtHigherVersion()
        {
            Assert.Throws<VersioningAmbiguityException>(() => AttributeHelper.GetPropertyAttributeValue<TestClassWithAttributes, MaxCollectionCountAttribute, int>
                (x => x.PropAmbiguityFromVersion3, attr => attr.MaxCount, 4));
        }

        [Fact]
        public void VersioningTestAmbiguityIgnored()
        {
            var value = AttributeHelper.GetPropertyAttributeValue<TestClassWithAttributes, MaxCollectionCountAttribute, int>
                (x => x.PropAmbiguityFromVersion3, attr => attr.MaxCount, 5);
            Assert.Equal(4, value);
        }

        [Fact]
        public void VersioningNoAttributes()
        {
            var value = AttributeHelper.GetPropertyAttributeValue<TestClassWithAttributes, MaxCollectionCountAttribute, int>
                (x => x.PropNoAttrubutes, attr => attr.MaxCount, 5);
            Assert.Equal(default, value);
        }
    }

    public class TestClassWithAttributes
    {
        [MaxCollectionCount(maxCount: 1, fromVersion: 1)]
        [MaxCollectionCount(maxCount: 2, fromVersion: 2)]
        [MaxCollectionCount(maxCount: 9, fromVersion: 5)]
        [MaxCollectionCount(maxCount: 3, fromVersion: 8)]
        public int PropAllGood { get; set; }

        [MaxCollectionCount(maxCount: 1, fromVersion: 1)]
        [MaxCollectionCount(maxCount: 2, fromVersion: 2)]
        [MaxCollectionCount(maxCount: 1, fromVersion: 3)]
        [MaxCollectionCount(maxCount: 3, fromVersion: 3)]
        [MaxCollectionCount(maxCount: 4, fromVersion: 5)]
        public int PropAmbiguityFromVersion3 { get; set; }

        public int PropNoAttrubutes { get; set; }


    }
}
