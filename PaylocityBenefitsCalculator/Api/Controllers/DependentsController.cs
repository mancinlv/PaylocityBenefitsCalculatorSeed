using Api.Dtos.Dependent;
using Api.Models;
using Application;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DependentsController : ControllerBase
    {
        private readonly IDependentService _dependentsService;
        public DependentsController(IDependentService dependentsService)
        {
            _dependentsService = dependentsService;
        }

        [SwaggerOperation(Summary = "Get dependent by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
        {
            var dependent = await _dependentsService.GetAsync(id);
            return HandleResponse(dependent);
        }

        // TODO want to get them by employee id? 
        [SwaggerOperation(Summary = "Get all dependents")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<IList<GetDependentDto>>>> GetAll()
        {
            var dependents = await _dependentsService.GetAllAsync();
            return HandleResponse(dependents);
        }

        [SwaggerOperation(Summary = "Add dependent")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<IList<GetDependentDto>>>> AddDependent(AddDependentWithEmployeeIdDto newDependent)
        {
            try
            {
                var dependents = await _dependentsService.AddAsync(newDependent);
                return HandleResponse(dependents);
            }
            catch (ArgumentException ex)
            {
                return ErrorResponse<IList<GetDependentDto>>(null, ex.Message);
            }
            
        }

        [SwaggerOperation(Summary = "Update dependent")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<IList<GetDependentDto>>>> UpdateDependent(int id, UpdateDependentDto updatedDependent)
        {
            try
            {
                var dependents = await _dependentsService.UpdateAsync(id, updatedDependent);
                return HandleResponse(dependents);
            }
            catch (ArgumentException ex)
            {
                return ErrorResponse<IList<GetDependentDto>>(null, ex.Message);
            }
            
        }

        [SwaggerOperation(Summary = "Delete dependent")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<IList<GetDependentDto>>>> DeleteDependent(int id)
        {
            var dependents = await _dependentsService.DeleteAsync(id);
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

        private static ApiResponse<T> ErrorResponse<T>(T data, string message)
        {
            return new ApiResponse<T>
            {
                Data = data,
                Success = false,
                Message = message
            };
        }
    }
}
