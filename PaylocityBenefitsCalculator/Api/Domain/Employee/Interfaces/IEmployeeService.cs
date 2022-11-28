using Api.Dtos.Employee;

namespace Api.Domain.Employee.Interfaces
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
}
