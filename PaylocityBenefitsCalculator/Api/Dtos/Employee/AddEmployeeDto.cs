using Api.Dtos.Dependent;

namespace Api.Dtos.Employee
{
    public class AddEmployeeDto : BasePersonDto
    {
        public decimal Salary { get; set; }
        public IList<AddDependentDto>? Dependents { get; set; }
    }
}
