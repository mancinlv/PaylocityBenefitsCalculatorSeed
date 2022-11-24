using Api.Domain.Enums;
using Api.Dtos.Dependent;

namespace Application
{
    // TODO add CRUD interface w/ generic? 

    public interface ICRUD
    {
        Task<T> GetAsync<T>(int id);
        Task<IList<T>> GetAllAsync<T>();
        Task<IList<T>> AddAsync<T>(T request);
        Task<IList<T>> UpdateAsync<T>(int id, T request);
        Task<IList<T>> DeleteAsync<T>(int id);
    }

    public interface IDependentService
    {
        Task<GetDependentDto> GetAsync(int id);
        Task<IList<GetDependentDto>> GetAllAsync();
        Task<IList<AddDependentWithEmployeeIdDto>> AddAsync(AddDependentWithEmployeeIdDto request);
        Task<IList<GetDependentDto>> UpdateAsync(int id, UpdateDependentDto request);
        Task<IList<GetDependentDto>> DeleteAsync(int id);
    }

    public class DependentService
    {
        public DependentService()
        {

        }

        public async Task<GetDependentDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<GetDependentDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<AddDependentWithEmployeeIdDto>> AddAsync(AddDependentWithEmployeeIdDto request)
        {
            throw new NotImplementedException();
            //bool canAdd = CanAdd(request);
        }

        public async Task<IList<GetDependentDto>> UpdateAsync(int id, UpdateDependentDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<GetDependentDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        //TODO Move to domain service?
        private static bool CanAdd(AddDependentWithEmployeeIdDto newDependent, IList<GetDependentDto> existing)
        {
            if (newDependent.Relationship == Relationship.Child) return true;
            var existingRelationship = existing.Where(y => y.Relationship == Relationship.Spouse || y.Relationship == Relationship.DomesticPartner).Select(x => x?.Relationship); // should just be one if validation working correctly
            return existingRelationship == null ? true : false; // can never add more than 1 spouse or partnership
        }
    }
}