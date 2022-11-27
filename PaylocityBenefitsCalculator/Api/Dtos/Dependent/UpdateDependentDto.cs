using Api.Domain.Enums;

namespace Api.Dtos.Dependent
{
    public class UpdateDependentDto : BasePersonDto
    {
        public Relationship Relationship { get; set; }
    }
}
