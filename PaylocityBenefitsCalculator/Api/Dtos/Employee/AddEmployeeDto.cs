using Api.Dtos.Dependent;

namespace Api.Dtos.Employee
{
    public class AddEmployeeDto
    {
        // Defaulting these to empty to avoid null ref exceptions, but i would add validation to make these fields required (Fluent Validation?) -- LVM
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<AddDependentDto>? Dependents { get; set; }
    }
}
