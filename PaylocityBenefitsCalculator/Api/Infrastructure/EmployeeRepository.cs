using Api.Domain.Employee.Interfaces;
using Api.Domain.Employee.Models;

namespace Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {

        public IList<EmployeeEntity> AllEmployees = new List<EmployeeEntity>
            {
                new()
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new()
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10),
                },
                new()
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17),
                }
            };

        public async Task<IList<EmployeeEntity>> GetAllAsync(){
            return AllEmployees;
        }

        public async Task<EmployeeEntity> GetAsync(int id){
            return AllEmployees.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IList<EmployeeEntity>> DeleteAsync(int id)
        {
            return AllEmployees.Where(x => x.Id != id).ToList();
        }
    }
}