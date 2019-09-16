using System;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Attributes;

namespace Testinator.TestSystem.Implementation
{
    public class TestInfo : ITestInfo
    {
        [StringLength(min: 10, max: 200, fromVersion: 1)]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditionDate { get; set; }

        public Category Category { get; set; }

        [TimeSpanLimit(
            minSeconds: 15,
            maxSeconds: 90 * 60,
            fromVersion: 1)]
        public TimeSpan TimeLimit { get; set; }
    }
}
