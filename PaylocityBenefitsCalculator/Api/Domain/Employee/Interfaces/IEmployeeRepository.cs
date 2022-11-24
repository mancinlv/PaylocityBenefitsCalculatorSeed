using Api.Domain.Employee.Models;

namespace Api.Domain.Employee.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity> GetAsync(int id);
        Task<IList<EmployeeEntity>> GetAllAsync();
    }
}