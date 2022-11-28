using Api.Domain.Dependent.Interfaces;
using Api.Domain.Dependent.Models;
using Api.Domain.Employee.Interfaces;
using Api.Domain.Employee.Models;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;

namespace Application
{
    public interface IEmployeeService
    {
        Task<GetEmployeeDto> GetAsync(int id);
        Task<IList<GetEmployeeDto>> GetAllAsync();
        Task<IList<GetEmployeeDto>> AddAsync(AddEmployeeDto employee);
        Task<IList<GetEmployeeDto>> UpdateAsync(int id, UpdateEmployeeDto employee);
        Task<IList<GetEmployeeDto>> DeleteAsync(int id);
        Task<PaycheckDto> GetBiMonthlyPaycheckAsync(int employeeId);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDependentRepository _dependentRepository;
        public EmployeeService(IEmployeeRepository employeeRepository,
            IDependentRepository dependentRepository)
        {
            _employeeRepository = employeeRepository;
            _dependentRepository = dependentRepository;
        }

        public async Task<GetEmployeeDto> GetAsync(int id)
        {
            EmployeeEntity employee = await _employeeRepository.GetAsync(id);
            var dependents = await _dependentRepository.GetAllByEmployeeIdAsync(id);
            //TODO fix list type https://medium.com/developers-arena/ienumerable-vs-icollection-vs-ilist-vs-iqueryable-in-c-2101351453db
            return ToEmployeeDto(employee, dependents);
        }

        public async Task<IList<GetEmployeeDto>> GetAllAsync()
        {
            IList<EmployeeEntity> employees = await _employeeRepository.GetAllAsync();
            var dependents = await _dependentRepository.GetAllAsync();
            return employees.Select(x => ToEmployeeDto(x, dependents.Where(d=> d.EmployeeId == x.Id))).ToList();
        }

        public async Task<IList<GetEmployeeDto>> AddAsync(AddEmployeeDto employee)
        {
            IList<EmployeeEntity> employees = await _employeeRepository.GetAllAsync();
            var dependents = await _dependentRepository.GetAllAsync();
            List<GetEmployeeDto> employeesWithDependents = employees.Select(x => ToEmployeeDto(x, dependents.Where(d => d.EmployeeId == x.Id))).ToList();
            employeesWithDependents.Add(ToEmployeeDto(employee, employees.Max(x=>x.Id), dependents.Max(y => y.Id)));
            return employeesWithDependents;
        }


        // Assuming that employee id does exist. Otherwise would handle not found/ null with some type of message
        public async Task<IList<GetEmployeeDto>> UpdateAsync(int id, UpdateEmployeeDto employee)
        {
            IList<EmployeeEntity> employees = await _employeeRepository.GetAllAsync();
            var dependents = await _dependentRepository.GetAllAsync();
            var employeesWithDependents = employees.Select(x => ToEmployeeDto(x, dependents.Where(d => d.EmployeeId == x.Id))).ToList();
            var employeeIndex = employeesWithDependents.FindIndex(x => x.Id == id);
            if (employeeIndex != -1)
            {
                employeesWithDependents[employeeIndex].FirstName = employee.FirstName;
                employeesWithDependents[employeeIndex].LastName = employee.LastName;
                employeesWithDependents[employeeIndex].Salary = employee.Salary;
                employeesWithDependents[employeeIndex].DateOfBirth = employee.DateOfBirth;
            }
            return employeesWithDependents;
        }

        public async Task<IList<GetEmployeeDto>> DeleteAsync(int id)
        {
            IList<EmployeeEntity> employees = await _employeeRepository.DeleteAsync(id);
            var dependents = await _dependentRepository.GetAllAsync();
            var employeesWithDependents = employees.Select(x => ToEmployeeDto(x, dependents.Where(d => d.EmployeeId == x.Id))).ToList();
            return employeesWithDependents;
        }

        public async Task<PaycheckDto> GetBiMonthlyPaycheckAsync(int employeeId)
        {
            EmployeeEntity employee = await _employeeRepository.GetAsync(employeeId);
            if (employee == null) return null!;
            var dependents = await _dependentRepository.GetAllByEmployeeIdAsync(employeeId);
            employee.Dependents = dependents;
            
            var baseEmployeeCost = employee.GetEmployeePaycheckBenefitsCost();
            var dependentsCost = employee.GetPaycheckCostOfDependents();
            var salarySurcharge = employee.GetPaycheckHighSalarySurcharge();
            var olderDependentCost = employee.GetDependentsOverFiftyYearsCost();
            var totalCost = baseEmployeeCost + dependentsCost + salarySurcharge + olderDependentCost;
            var payment = Math.Round(employee.Salary / 26, 2) - totalCost;

            var paycheck = new PaycheckDto
            {
                Payment = payment,
                EmployeeBenefitCost = baseEmployeeCost,
                DependentsBenefitCost = dependentsCost,
                SalarySurchargeCost = salarySurcharge,
                DependentsOverFiftyCost = olderDependentCost,
                TotalBenefitsCost = totalCost
            };
            return paycheck;
        }

        //TODO move mapping
        //would never have these ids set this way w/ db
        private GetEmployeeDto ToEmployeeDto(AddEmployeeDto employee, int maxEmployeeId, int maxDependentId)
        {
            var newEmployeeId = ++maxEmployeeId;
            return new GetEmployeeDto
            {
                Id = newEmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary,
                Dependents = MapDependents(employee.Dependents, maxDependentId, newEmployeeId)
            };
        }


        private GetEmployeeDto ToEmployeeDto(EmployeeEntity employee, IEnumerable<DependentEntity> dependents)
        {
            return new GetEmployeeDto
            {
                
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = dependents?.Select(x => ToDependentDto(x))?.ToList()
            };
        }

        private GetDependentDto ToDependentDto(DependentEntity x)
        {
            return new GetDependentDto
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                Relationship = x.Relationship
            };
        }

        //choosing to return empty list rather than null
        private IList<GetDependentDto>? MapDependents(IList<AddDependentDto> dependents, int maxDependentId, int newEmployeeId)
        {
            var dependentsDto = new List<GetDependentDto>();
            if (dependents == null) return dependentsDto;
            foreach (var dependent in dependents)
            {
                var d = new GetDependentDto
                {
                    Id = ++maxDependentId,
                    EmployeeId = newEmployeeId,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    DateOfBirth = dependent.DateOfBirth,
                    Relationship = dependent.Relationship
                };
                dependentsDto.Add(d);
            }
            return dependentsDto;
        }
    }
}