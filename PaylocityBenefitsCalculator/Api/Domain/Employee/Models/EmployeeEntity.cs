using Api.Domain.Dependent.Models;
using Api.Domain.ValueTypes;
namespace Api.Domain.Employee.Models
{
    public class EmployeeEntity : Person
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public ICollection<DependentEntity> Dependents { get; set; } = new List<DependentEntity>();

        public decimal GetEmployeePaycheckBenefitsCost()
        {
            return (1000 * 12) / 26;
        }

        public decimal GetDependentsOverFiftyYearsCost()
        {
            //test: 1 under, 1 over, 2 - over and under, 2 both over, 2 both under
            var numberOfOldies = Dependents.Where(x => x.IsOverFiftyYears()).Count();
            return ((numberOfOldies * 200) * 12) / 26;
        }

        public decimal GetPaycheckCostOfDependents()
        {
            //test: count = 0, 1, 5
            return (Dependents.Count * 600m) / 26;
        }

        public decimal GetPaycheckHighSalarySurcharge()
        {
            // test: salary = 75000, 80000, 81000
            decimal surcharge = 0m;
            if (Salary > 80000)
            {
                surcharge = (Salary * .02m) / 26;
            }
            return surcharge;
        }
    }
}
