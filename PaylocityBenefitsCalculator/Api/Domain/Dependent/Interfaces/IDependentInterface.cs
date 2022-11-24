using Api.Domain.Dependent.Models;

namespace Api.Domain.Dependent.Interfaces
{
    public interface IDependentInterface
    {
        Task<IList<DependentEntity>> GetAllAsync();
        Task<DependentEntity> GetAsync(int id);
        Task<IEnumerable<DependentEntity>> GetAllAsyncByEmployeeId(int employeeId);
    }
}
