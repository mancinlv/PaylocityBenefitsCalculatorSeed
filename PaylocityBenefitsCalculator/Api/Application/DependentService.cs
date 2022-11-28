using Api.Domain.Dependent.Interfaces;
using Api.Domain.Dependent.Models;
using Api.Dtos.Dependent;

namespace Application
{
    // TODO add CRUD interface w/ generic? -LVM
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
        Task<IList<GetDependentDto>> AddAsync(AddDependentWithEmployeeIdDto request);
        Task<IList<GetDependentDto>> UpdateAsync(int id, UpdateDependentDto request);
        Task<IList<GetDependentDto>> DeleteAsync(int id);
        Task<IList<GetDependentDto>> GetAllByEmployeeIdAsync(int employeeId);
    }

    public class DependentService : IDependentService
    {
        private readonly IDependentRepository _dependentRepository;
        public DependentService(IDependentRepository dependentRepository)
        {
            _dependentRepository = dependentRepository;
        }

        public async Task<GetDependentDto> GetAsync(int id)
        {
            var dependent = await _dependentRepository.GetAsync(id);
            return ToDependentDto(dependent);
        }

        public async Task<IList<GetDependentDto>> GetAllAsync()
        {
            var dependents = await _dependentRepository.GetAllAsync();
            return dependents.Select(x => ToDependentDto(x)).ToList();
        }

        public async Task<IList<GetDependentDto>> GetAllByEmployeeIdAsync(int employeeId)
        {
            var employeeDependents = await _dependentRepository.GetAllByEmployeeIdAsync(employeeId);
            return employeeDependents?.Select(x => ToDependentDto(x)).ToList();
        }

        // Handle invalid relationship type better -LVM
        public async Task<IList<GetDependentDto>> AddAsync(AddDependentWithEmployeeIdDto request)
        {
            var existingDependents = await _dependentRepository.GetAllByEmployeeIdAsync(request.EmployeeId);
            var entity = ToDependentDto(request);
            bool canAdd = entity.CanAddRelationshipType(existingDependents);
            if (canAdd)
            {
                var updatedDependents = await _dependentRepository.AddAsync(ToDependentDto(request));
                return updatedDependents.Select(x => ToDependentDto(x)).ToList();
            }
            throw new ArgumentException($"A {request.Relationship} cannot be added to Employee # {request.EmployeeId}");
        }

        // Handle invalid relationship type better -LVM
        public async Task<IList<GetDependentDto>> UpdateAsync(int id, UpdateDependentDto request)
        {
            var existingDependents = await _dependentRepository.GetAllByEmployeeIdAsync(request.EmployeeId);
            var entity = ToDependentDto(request, id);
            bool canAdd = entity.CanAddRelationshipType(existingDependents);
            if (canAdd)
            {
                var updatedDependents = await _dependentRepository.UpdateAsync(ToDependentDto(request, id));
                return updatedDependents.Select(x => ToDependentDto(x)).ToList();
            }
            throw new ArgumentException($"A {request.Relationship} cannot be added to Employee # {request.EmployeeId}");
        }

        public async Task<IList<GetDependentDto>> DeleteAsync(int id)
        {
            var remainingDependents = await _dependentRepository.DeleteAsync(id);
            return remainingDependents.Select(x => ToDependentDto(x)).ToList();
        }


        private DependentEntity ToDependentDto(AddDependentWithEmployeeIdDto dependent)
        {
            return new DependentEntity
            {
                EmployeeId = dependent.EmployeeId,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship,
                DateOfBirth = dependent.DateOfBirth,
            };
        }

        private DependentEntity ToDependentDto(UpdateDependentDto dependent, int id)
        {
            return new DependentEntity
            {
                Id = id,
                EmployeeId = dependent.EmployeeId,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship
            };
        }

        private GetDependentDto ToDependentDto(DependentEntity dependent)
        {
            return new GetDependentDto
            {
                Id = dependent.Id,
                EmployeeId = dependent.EmployeeId,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                Relationship = dependent.Relationship,
                DateOfBirth = dependent.DateOfBirth

            };
        }
    }
}