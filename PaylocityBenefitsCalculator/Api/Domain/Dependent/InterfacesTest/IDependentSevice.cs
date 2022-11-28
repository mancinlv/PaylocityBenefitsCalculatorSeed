using Api.Dtos.Dependent;

namespace Api.Domain.Dependent.Interfaces
{  
    // TODO add CRUD interface w/ generics?
   //public interface ICRUD
   //{
   //    Task<T> GetAsync<T>(int id);
   //    Task<IList<T>> GetAllAsync<T>();
   //    Task<IList<T>> AddAsync<T>(T request);
   //    Task<IList<T>> UpdateAsync<T>(int id, T request);
   //    Task<IList<T>> DeleteAsync<T>(int id);
   //}

    public interface IDependentService
    {
        Task<GetDependentDto> GetAsync(int id);
        Task<IList<GetDependentDto>> GetAllAsync();
        Task<IList<GetDependentDto>> AddAsync(AddDependentWithEmployeeIdDto request);
        Task<IList<GetDependentDto>> UpdateAsync(int id, UpdateDependentDto request);
        Task<IList<GetDependentDto>> DeleteAsync(int id);
        Task<IList<GetDependentDto>> GetAllByEmployeeIdAsync(int employeeId);
    }

}
