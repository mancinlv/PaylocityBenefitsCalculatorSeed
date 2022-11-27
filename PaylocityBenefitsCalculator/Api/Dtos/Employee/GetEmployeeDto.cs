using Api.Dtos.Dependent;

namespace Api.Dtos.Employee
{
    public class GetEmployeeDto : BasePersonDto
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public IList<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();
    }
}
