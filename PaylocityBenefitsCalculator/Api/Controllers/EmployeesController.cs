using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Application;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDependentService _dependentService;

        public EmployeesController(IEmployeeService employeeService,
            IDependentService dependentService)
        {
            _employeeService = employeeService;
            _dependentService = dependentService;
        }

        [SwaggerOperation(Summary = "Get employee by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
        {
            var employee = await _employeeService.GetAsync(id);

            //TODO handle base response in base controller
            return HandleResponse(employee);
        }

        
        [SwaggerOperation(Summary = "Get all employees")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<IList<GetEmployeeDto>>>> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return HandleResponse(employees);
        }

        ///in real life with state management on client side, i would not return all employees here. would just return the added employee -- LVM
        // changed response to be GetEmployeeDto so that Ids are available -- LVM
        [SwaggerOperation(Summary = "Add employee")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<IList<GetEmployeeDto>>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            var employees = await _employeeService.AddAsync(newEmployee);
            return HandleResponse(employees);
        }

        // returning all atm for front end. would not do this w/ more time
        [SwaggerOperation(Summary = "Update employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<IList<GetEmployeeDto>>>> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            var employees = await _employeeService.UpdateAsync(id, updatedEmployee);
            return HandleResponse(employees);
        }

        //TODO if employee does not exist, handle
        [SwaggerOperation(Summary = "Delete employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<IList<GetEmployeeDto>>>> DeleteEmployee(int id)
        {
            var employees = await _employeeService.DeleteAsync(id);
            return HandleResponse(employees);
        }

        [HttpGet("{id}/paycheck")]
        public async Task<ActionResult<ApiResponse<PaycheckDto>>> GetPaycheck(int id)
        {
            var paycheck = await _employeeService.GetBiMonthlyPaycheckAsync(id);
            return HandleResponse(paycheck);
        }

        //Might put this in another controller - LVM
        [SwaggerOperation(Summary = "Get all dependents for given employee")]
        [HttpGet("{id}/dependents")]
        public async Task<ActionResult<ApiResponse<IList<GetDependentDto>>>> GetAllEmployeeDependents(int id)
        {
            var dependents = await _dependentService.GetAllByEmployeeIdAsync(id);
            return HandleResponse(dependents);
        }

        //TODO moved to shared base controller
        private static ApiResponse<T> HandleResponse<T>(T data)
        {
            return new ApiResponse<T>
            {
                Data = data,
                Success = true
            };
        }

    }
}
