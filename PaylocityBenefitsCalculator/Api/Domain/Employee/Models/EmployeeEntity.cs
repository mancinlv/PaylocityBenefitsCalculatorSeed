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
            var numberOfOldies = Dependents.Where(x => x.IsOverFiftyYears()).Count();
            return Math.Round(((numberOfOldies * 200) * 12) / 26m, 2);
        }

        public decimal GetPaycheckCostOfDependents()
        {
            return Math.Round((Dependents.Count * 600) * 12 / 26m, 2);
        }

        public decimal GetPaycheckHighSalarySurcharge()
        {
            decimal surcharge = 0m;
            if (Salary > 80000)
            {
                surcharge = Math.Round((Salary * .02m) / 26m, 2);
            }
            return surcharge;
        }
    }
}
