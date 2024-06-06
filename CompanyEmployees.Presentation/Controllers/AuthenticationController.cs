using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController
{
		private readonly IServiceManager _service;

		public AuthenticationController(IServiceManager service) => _service = service;
}
