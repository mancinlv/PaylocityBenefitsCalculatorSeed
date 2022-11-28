using Api.Domain.Enums;
using Api.Domain.ValueTypes;

namespace Api.Domain.Dependent.Models
{
    public class DependentEntity : Person
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Relationship Relationship { get; set; }
        // public Employee? Employee { get; set; } not needed

        public bool IsOverFiftyYears()
        {
            DateTime today = DateTime.Today;
            int age = DateTime.Now.Year - DateOfBirth.Year;
            if (today.Month < DateOfBirth.Month || today.Month == DateOfBirth.Month && today.Day < DateOfBirth.Day) age--;
            return age >= 50;
        }

        public bool CanAddRelationshipType(IList<DependentEntity>? existing)
        {
            if (Relationship == Relationship.Child) return true;
            var existingNonChildRelationship = existing?.FirstOrDefault(y => y.Relationship == Relationship.Spouse || y.Relationship == Relationship.DomesticPartner);
            if (existingNonChildRelationship != null && existingNonChildRelationship.Id != Id) return false;
            return true;
        }
    }
}
