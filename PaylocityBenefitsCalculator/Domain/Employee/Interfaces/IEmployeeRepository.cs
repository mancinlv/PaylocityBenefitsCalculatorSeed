namespace Domain.Employee
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetAsync(int id);
        Task<IList<Employee>> GetAllAsync()
    }
}