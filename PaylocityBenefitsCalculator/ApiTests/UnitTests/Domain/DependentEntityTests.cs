using Api.Domain.Dependent.Models;
using Api.Domain.Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
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

        [Fact]
        public static void CanAddRelationshipType_DuplicatePartner_ReturnsFalse()
        {
            var originalDependent = new DependentEntity { Relationship = Relationship.DomesticPartner };
            var newDependent = new DependentEntity { Relationship = Relationship.DomesticPartner };

            var acutal = newDependent.CanAddRelationshipType(new List<DependentEntity> { originalDependent });
            acutal.Should().BeFalse();
        }

        [Fact]
        public static void CanAddRelationshipType_ExistingPartner_NewSpouse_ReturnsFalse()
        {
            var originalDependent = new DependentEntity { Relationship = Relationship.DomesticPartner };
            var newDependent = new DependentEntity { Relationship = Relationship.Spouse };

            var acutal = newDependent.CanAddRelationshipType(new List<DependentEntity> { originalDependent });
            acutal.Should().BeFalse();
        }

        [Fact]
        public static void CanAddRelationshipType_NullExisting_ReturnsTrue()
        {
            var newDependent = new DependentEntity { Relationship = Relationship.Spouse };
            var acutal = newDependent.CanAddRelationshipType(null);
            acutal.Should().BeTrue();
        }

        [Fact]
        public static void CanAddRelationshipType_NoExisting_ReturnsTrue()
        {
            var newDependent = new DependentEntity { Relationship = Relationship.Spouse };
            var acutal = newDependent.CanAddRelationshipType(new List<DependentEntity>());
            acutal.Should().BeTrue();
        }

        [Fact]
        public static void CanAddRelationshipType_Child_ReturnsTrue()
        {
            var originalDependent = new DependentEntity { Relationship = Relationship.Child };
            var newDependent = new DependentEntity { Relationship = Relationship.Spouse };
            var acutal = newDependent.CanAddRelationshipType(new List<DependentEntity>());
            acutal.Should().BeTrue();
        }
    }
}
