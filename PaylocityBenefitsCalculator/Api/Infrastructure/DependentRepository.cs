using Api.Domain.Dependent.Interfaces;
using Api.Domain.Dependent.Models;
using Api.Domain.Enums;

namespace Api.Infrastructure
{
    public class DependentRepository : IDependentRepository
    {
        public IList<DependentEntity> AllDependents = new List<DependentEntity>
        {
            new()
            {
                Id = 1,
                EmployeeId = 2,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3)
                },
            new()
            {
                Id = 2,
                EmployeeId = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23)
            },
            new()
            {
                Id = 3,
                EmployeeId = 2,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18)
            },
            new()
            {
                Id = 4,
                EmployeeId = 3,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2)
            }
        };

        public async Task<IList<DependentEntity>> GetAllAsync()
        {
            return AllDependents;
        }

        public async Task<DependentEntity> GetAsync(int id)
        {
            return AllDependents.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IList<DependentEntity>> GetAllByEmployeeIdAsync(int employeeId)
        {
            return AllDependents.Where(x => x.EmployeeId == employeeId)?.ToList();
        }
    }
}
