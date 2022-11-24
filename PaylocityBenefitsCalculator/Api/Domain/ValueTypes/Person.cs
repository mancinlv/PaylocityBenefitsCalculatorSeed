namespace Api.Domain.ValueTypes
{
    public class Person
    {
        public string? FirstName { get; set; } //why using ? here
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}