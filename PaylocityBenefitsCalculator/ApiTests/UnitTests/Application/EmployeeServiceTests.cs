using Api.Domain.Dependent.Interfaces;
using Api.Domain.Dependent.Models;
using Api.Domain.Employee.Interfaces;
using Api.Domain.Employee.Models;
using Api.Domain.Enums;
using Application;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests.Application
{
    public class EmployeeServiceTests
    {
        private IEmployeeRepository _employeeRepoSub;
        private IDependentRepository _dependentRepoSub;
        private readonly IEmployeeService _sut;

        public EmployeeServiceTests()
        {
            _employeeRepoSub = Substitute.For<IEmployeeRepository>();
            _dependentRepoSub = Substitute.For<IDependentRepository>();
            _sut = new EmployeeService(_employeeRepoSub, _dependentRepoSub);
        }


        [Fact]
        public async Task GetBiMonthlyPaycheckAsync_NoEmployee_ReturnsNull()
        {
            _employeeRepoSub.GetAsync(Arg.Any<int>()).Returns((Task<EmployeeEntity>)await Task.FromResult<IList<EmployeeEntity>>(null));
            var actual = await _sut.GetBiMonthlyPaycheckAsync(Arg.Any<int>());
            using (new AssertionScope())
            {
                actual.Should().BeNull();
                await _dependentRepoSub.Received(0).GetAllByEmployeeIdAsync(Arg.Any<int>());
            }
        }

        [Fact]
        public async Task GetBiMonthlyPaycheckAsync_ReturnsCorrectAmounts()
        {
            _employeeRepoSub.GetAsync(Arg.Any<int>()).Returns(await Task.FromResult(new EmployeeEntity
            {
                Id = 1,
                Salary = 100000m

            }));
            _dependentRepoSub.GetAllByEmployeeIdAsync(Arg.Any<int>()).Returns(new List<DependentEntity>
                {
                    new DependentEntity
                    {
                        DateOfBirth = DateTime.Today.AddYears(-30),
                        Relationship = Relationship.Spouse
                    },
                    new DependentEntity
                    {
                        DateOfBirth = DateTime.Today.AddYears(-2),
                        Relationship = Relationship.Child
                    }
                });

            var actual = await _sut.GetBiMonthlyPaycheckAsync(1);

            using (new AssertionScope())
            {
                actual.Should().NotBeNull();
                actual.Payment.Should().Be(2754.38M);
                actual.EmployeeBenefitCost.Should().Be(461m);
                actual.DependentsBenefitCost.Should().Be(553.85m);
                actual.SalarySurchargeCost.Should().Be(76.92m);
                actual.DependentsOverFiftyCost.Should().Be(0);
                actual.TotalBenefitsCost.Should().Be(actual.EmployeeBenefitCost + actual.DependentsBenefitCost + actual.SalarySurchargeCost + actual.DependentsOverFiftyCost);
            }
        }
    }
}

