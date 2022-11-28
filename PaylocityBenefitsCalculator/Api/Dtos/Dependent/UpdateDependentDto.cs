using Api.Domain.Enums;

namespace Api.Dtos.Dependent
{
    public class UpdateDependentDto : BasePersonDto
    {
        public int EmployeeId { get; set; }
        public Relationship Relationship { get; set; }
    }
}
