namespace Api.Dtos.Employee
{
    public class PaycheckDto
    {
        public decimal Payment { get; set; }
        public decimal TotalBenefitsCost { get; set; }
        public decimal EmployeeBenefitCost { get; set; }
        public decimal DependentsBenefitCost { get; set; }
        public decimal SalarySurchargeCost { get; set; }
        public decimal DependentsOverFiftyCost { get; set; }
    }
}
