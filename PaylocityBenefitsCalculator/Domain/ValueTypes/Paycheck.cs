namespace Domain.ValueTypes
{
    public class Paycheck
    {
        public decimal Payment { get; init; }
        public decimal TotalBenefitsCost {get; init;}
        public decimal EmployeeBenefitCost { get; init; }
        public decimal DependentsBenefitCost { get; init; }
        public decimal SalarySurchargeCost { get; init; }
        public decimal OverFiftyCost { get; init; }
    }
}