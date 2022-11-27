using Api.Domain.Enums;

namespace Api.Dtos.Dependent
{    
    public class GetDependentDto : BasePersonDto
    {
        public int Id { get; set; }
        //added for easy reference - LVM
        public int EmployeeId { get; set; }
        public Relationship Relationship { get; set; }
    }
}
