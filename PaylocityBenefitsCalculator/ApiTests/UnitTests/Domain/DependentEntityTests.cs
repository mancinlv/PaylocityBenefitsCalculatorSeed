using Api.Domain.Dependent.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests.Domain
{
    public class DependentEntityTests
    {
        [Fact]
        public static void IsOverFiftyYears_30_ReturnsFalse()
        {
            var under50 = new DependentEntity
            {
                DateOfBirth = DateTime.Today.AddYears(-30)
            };
            var actual = under50.IsOverFiftyYears();

            actual.Should().BeFalse();
        }

        [Fact]
        public static void IsOverFiftyYears_60_ReturnsTrue()
        {
            var over50 = new DependentEntity
            {
                DateOfBirth = DateTime.Today.AddYears(-60)
            };
            var actual = over50.IsOverFiftyYears();

            actual.Should().BeTrue();
        }

        [Fact]
        public static void IsOverFiftyYears_50LaterThisYear_ReturnsFalse()
        {
            var under50 = new DependentEntity
            {
                DateOfBirth = DateTime.Today.AddDays(+3).AddYears(-50)
            };
            var actual = under50.IsOverFiftyYears();

            actual.Should().BeFalse();
        }

        [Fact]
        public static void IsOverFiftyYears_50YesterdayThisYear_ReturnsTrue()
        {
            var under50 = new DependentEntity
            {
                DateOfBirth = DateTime.Today.AddDays(-1).AddYears(-50)
            };
            var actual = under50.IsOverFiftyYears();

            actual.Should().BeTrue();
        }
    }
}
