using Api.Domain.Enums;
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

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [SwaggerOperation(Summary = "Get employee by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
        {
            var employee = await _employeeService.GetAsync(id);

            //TODO handle base response in base controller
            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Get all employees")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<IList<GetEmployeeDto>>>> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            var result = new ApiResponse<IList<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };

            return result;
        }

        ///in real life with state management on client side, i would not return all employees here. would just return the added employee
        // changed response to be GetEmployeeDto so that Ids are available
        [SwaggerOperation(Summary = "Add employee")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<IList<GetEmployeeDto>>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            var employees = await _employeeService.AddAsync(newEmployee);
            var result = new ApiResponse<IList<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };

            return result;
        }

        //if employee does not exist, handle
        [SwaggerOperation(Summary = "Update employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            throw new NotImplementedException();
        }

        //TODO if employee does not exist, handle
        [SwaggerOperation(Summary = "Delete employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<IList<GetEmployeeDto>>>> DeleteEmployee(int id)
        {
            var employees = await _employeeService.DeleteAsync(id);
            var result = new ApiResponse<IList<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };

            return result;
        }

        [HttpGet("{id}/paycheck")]
        public async Task<ActionResult<ApiResponse<decimal>>> GetPaycheck(int employeeId)
        {
            throw new NotImplementedException();
        }

        // Might put this in another controller
        [SwaggerOperation(Summary = "Get all dependents for given employee")]
        [HttpGet("{id}/dependents")]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAllEmployeeDependents(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
