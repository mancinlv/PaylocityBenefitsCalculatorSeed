namespace Employee.Models
{
    public class Employee : Person
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

        public static decimal GetEmployeePaycheckBenefitsCost(){
            return (1000 x 12) / 26;
        }

        public static decimal GetDependentsOverFiftyYearsCost(){
            //test: 1 under, 1 over, 2 - over and under, 2 both over, 2 both under
            var numberOfOldies = Dependents.Where(x => x.IsOverFiftyYears()).Count();
            return ((numberOfOldies x 200) x 12) / 26; 
        }

        public static decimal GetPaycheckCostOfDependents()
        {
            //test: count = 0, 1, 5
            return (Dependents.Count x 600m) / 26;
        }

        public static decimal GetPaycheckHighSalarySurcharge(){
            // test: salary = 75000, 80000, 81000
            decimal surcharge = 0m;
            if(Salary > 80000) {
                surcharge = (Salary x .02m) / 26;
            }
            return surcharge;
        }
    }
}
