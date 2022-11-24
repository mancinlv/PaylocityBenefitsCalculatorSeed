using Api.Domain.Dependent.Models;
using Api.Domain.Employee.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests.Domain
{
    public class EmployeeEntityTests
    {

        [Fact]
        public void GetDependentsOverFiftyYearsCost_OneOverOneUnder_ReturnsCorrectAmount()
        {
            var employee = new EmployeeEntity
            {
                Dependents = new List<DependentEntity> { under50, over50}
            };

            decimal actual = employee.GetDependentsOverFiftyYearsCost();
            //1 * 200 * 12 / 26
            actual.Should().Be(92.31m);
        }

        [Fact]
        public void GetDependentsOverFiftyYearsCost_TwoOver_ReturnsCorrectAmount()
        {
            var employee = new EmployeeEntity
            {
                Dependents = new List<DependentEntity> { over50, over50 }
            };

            decimal actual = employee.GetDependentsOverFiftyYearsCost();
            //2 * 200 * 12 / 26
            actual.Should().Be(184.62m);
        }

        [Fact]
        public void GetDependentsOverFiftyYearsCost_TwoUnder_ReturnsCorrectAmount()
        {
            var employee = new EmployeeEntity
            {
                Dependents = new List<DependentEntity> { under50, under50 }
            };

            decimal actual = employee.GetDependentsOverFiftyYearsCost();
            //0 * 200 * 12 / 26
            actual.Should().Be(0);
        }

        [Fact]
        public void GetPaycheckCostOfDependents_Two_ReturnsCorrectAmount()
        {
            var employee = new EmployeeEntity
            {
                Dependents = new List<DependentEntity> { under50, under50 }
            };

            decimal actual = employee.GetPaycheckCostOfDependents();

            actual.Should().Be(553.85m);
        }

        [Fact]
        public void GetPaycheckCostOfDependents_Zero_ReturnsCorrectAmount()
        {
            var employee = new EmployeeEntity
            {
                Dependents = new List<DependentEntity> ()
            };

            decimal actual = employee.GetPaycheckCostOfDependents();

            actual.Should().Be(0);
        }

        [Fact]
        public void GetPaycheckHighSalarySurcharge_None_ReturnsCorrectAmount()
        {
            var employee = new EmployeeEntity
            {
               Salary = 50000
            };

            decimal actual = employee.GetPaycheckHighSalarySurcharge();

            actual.Should().Be(0);
        }

        [Fact]
        public void GetPaycheckHighSalarySurcharge_HasSurcharge_ReturnsCorrectAmount()
        {
            var employee = new EmployeeEntity
            {
                Salary = 81000
            };

            decimal actual = employee.GetPaycheckHighSalarySurcharge();

            actual.Should().Be(62.31m);
        }

        public DependentEntity over50 = new DependentEntity
        {
            DateOfBirth = DateTime.Today.AddYears(-60)
        };

        public DependentEntity under50 = new DependentEntity
        {
            DateOfBirth = DateTime.Today.AddYears(-20)
        };
    }


}
