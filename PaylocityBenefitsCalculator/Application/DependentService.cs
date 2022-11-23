namespace Application
{
    // TODO add CRUD interface w/ generic? 

    public interface ICRUD 
    {
        Task<T> GetAsync<T>(int id);
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> AddAsync<T>(T request);
        Task<IList<T>> UpdateAsync<T>(int id, T request);
        Task<IList<T>> DeleteAsync<T>(int id);
    }

    public interface IDependentService 
    {
        Task<GetDependentDto> GetAsync(int id);
        Task<IList<GetDependentDto>> GetAllAsync();
        Task<IList<AddDependentWithEmployeeIdDto>> AddAsync(AddDependentWithEmployeeIdDto request);
        Task<IList<GetDependentDto>> UpdateAsync(int id, UpdateDependent request);
        Task<IList<GetDependentDto>> DeleteAsync(int id);
    }

    public class DependentService
    {
        public DependentService(Parameters)
        {
            
        }

         public async Task<GetDependentDto> GetAsync(int id){

        }

        public async Task<IList<GetDependentDto>> GetAllAsync(){

        }

        public async Task<IList<AddDependentWithEmployeeIdDto>> AddAsync(AddDependentWithEmployeeIdDto request){
            bool canAdd = CanAdd(request);
        }

        public async Task<IList<GetDependentDto>> UpdateAsync(int id, UpdateDependent request){

        }

        public async Task<IList<GetDependentDto>> DeleteAsync(int id){

        }

        //TODO Move to domain service?
        private static bool CanAdd(AddDependentWithEmployeeIdDto new, IList<GetDependentDto> existing){
            if(new.Relationship == Relationship.Child) return true;
            var existingRelationship = existing.Where(y.Relationship == Relationship.Spouse || y.Relationship == Relationship.DomesticPartner).Select(x => x?.Relationship); // should just be one if validation working correctly
            return existingRelationship == null ? true : false; // can never add more than 1 spouse or partnership
        }
    }
}