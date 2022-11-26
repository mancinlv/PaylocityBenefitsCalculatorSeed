using Api.Domain.Dependent.Models;

namespace Api.Domain.Dependent.Interfaces
{
    public interface IDependentRepository
    {
        Task<IList<DependentEntity>> GetAllAsync();
        Task<DependentEntity> GetAsync(int id);
        Task<IList<DependentEntity>> AddAsync(DependentEntity dependent);
        Task<IList<DependentEntity>> UpdateAsync(DependentEntity dependent);
        Task<IList<DependentEntity>> DeleteAsync(int id);
        Task<IList<DependentEntity>> GetAllByEmployeeIdAsync(int employeeId);
    }
}
