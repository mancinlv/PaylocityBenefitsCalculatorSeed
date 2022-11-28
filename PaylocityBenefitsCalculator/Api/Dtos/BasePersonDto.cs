namespace Api.Dtos
{
    public class BasePersonDto
    {
        // Defaulting these to empty to avoid null ref exceptions, but i would add validation to make these fields required
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
