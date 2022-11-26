using Api.Domain.Enums;

namespace Api.Dtos.Dependent
{    
    public class GetDependentDto
    {
        public int Id { get; set; }
        //added for easy reference - LVM
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Relationship Relationship { get; set; }
    }
}
