using Api.Domain.Dependent.Models;

namespace Api.Domain.Dependent.Interfaces
{
    public interface IDependentRepository
    {
        Task<IList<DependentEntity>> GetAllAsync();
        Task<DependentEntity> GetAsync(int id);
        Task<IList<DependentEntity>> GetAllByEmployeeIdAsync(int employeeId);
    }
}
