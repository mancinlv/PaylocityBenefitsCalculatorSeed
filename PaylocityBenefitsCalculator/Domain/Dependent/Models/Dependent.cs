namespace Domain.Dependent.Models
{
    public class Dependent : Person
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } // FK
        public Relationship Relationship { get; set; }
       // public Employee? Employee { get; set; } no


        private static bool IsOverFiftyYears(){
            DateTime today = DateTime.Today;
            int age = DateTime.Now.Year - DateOfBirth.Year; 
            //if(DateOfBirth.Date > today.AddYears(-age))
            if (today.Month < DateOfBirth.Month || today.Month == DateOfBirth.Month && now.Day < DateOfBirth.Day) age--;
            return age >= 50;

            //var today = DateTime.Today;
            // Calculate the age.
            // var age = today.Subtract(DateOfBirth).TotalDays;

            // var years = (age / 365);

            // Math.Round(years);
        }
    }
}
