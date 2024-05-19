using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId, 
            [FromQuery] EmployeeParameters employeeParameters)
        {
            var employees = _service.EmployeeService.GetEmployeesAsync(companyId, 
                employeeParameters, trackChanges: false);

            return Ok(employees);
        }

        [HttpGet("id:guid", Name = "GetEmployeeFoxrCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = _service.EmployeeService.GetEmployeeAsync(companyId, id, trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            var employeeToReturn =
                    _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            _service.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public IActionResult UpdateEmployeeForUpdate(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
        {
            _service.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee,
                compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
            [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id, 
                compTrackChanges: false, empTrackChanges: true);

            TryValidateModel(result.employeeToPatch);

            patchDoc.ApplyTo(result.employeeToPatch);

            if (!ModelState.IsValid) return 
                    UnprocessableEntity(ModelState);

            _service.EmployeeService.SaveChangesForPatch(result.employeeToPatch, result.employeeEntity);

            return NoContent();
        }
    }
}