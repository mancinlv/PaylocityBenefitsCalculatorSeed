using Api.Domain.Employee.Interfaces;
using Api.Domain.Employee.Models;
using Api.Dtos.Employee;

namespace Application
{
    public interface IEmployeeService
    {
        Task<GetEmployeeDto> GetAsync(int id);
        Task<IList<GetEmployeeDto>> GetAllAsync();
        Task<IList<AddEmployeeDto>> AddAsync(AddEmployeeDto employee);
        Task<IList<GetEmployeeDto>> UpdateAsync(int id, UpdateEmployeeDto employee);
        Task<IList<GetEmployeeDto>> DeleteAsync(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<GetEmployeeDto> GetAsync(int id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            return new GetEmployeeDto();
        }

        public async Task<IList<GetEmployeeDto>> GetAllAsync()
        {
            throw new NotImplementedException();
            //var employees = await _employeeRepository.GetAllAsync();
        }

        public async Task<IList<AddEmployeeDto>> AddAsync(AddEmployeeDto employee)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<GetEmployeeDto>> UpdateAsync(int id, UpdateEmployeeDto employee)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<GetEmployeeDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetBiMonthlyPaycheckAsync(int employeeId)
        {
            // TODO ability to load dependents separately? 
            EmployeeEntity employee = await _employeeRepository.GetAsync(employeeId);
            decimal totalBenefitsCost = employee.GetEmployeePaycheckBenefitsCost();
            totalBenefitsCost += employee.GetPaycheckCostOfDependents();
            totalBenefitsCost += employee.GetPaycheckHighSalarySurcharge();
            totalBenefitsCost += employee.GetDependentsOverFiftyYearsCost();
            return employee.Salary = totalBenefitsCost;

            //TODO return full paycheck response model w/ line items

            // Maria + 2; Salary 100,000, 32 years
            // Maria base => 1000 x 12 / 26 = 461
            // Dependents base => 600 x 2 x 12 / 26 = 553.846 (what do do here?! round up?!)
            // $2000 extra x year (/26) = 76.923
            // $0 no old age
            // = $1091.769

            // 1000 x 12 / 26
            // (600 x n) x 12 / 26=
            // 200 x 12 / 26
        }
    }
}