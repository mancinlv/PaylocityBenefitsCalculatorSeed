using Api.Domain.Enums;

namespace Api.Dtos.Dependent
{
    public class AddDependentDto : BasePersonDto
    {
        public Relationship Relationship { get; set; }
    }
}
