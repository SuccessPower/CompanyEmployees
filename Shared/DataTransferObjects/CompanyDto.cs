using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
		public class CompanyDto
		{
				public Guid Id { get; init; }
				public string? Name { get; init; }
				public string? FullAddress { get; init; }
		}
		//[Serializable]
		//public record CompanyDto(Guid Id, string Name, string FullAddress);
		public record CompanyForCreationDto(string Name, string Address, string Country);
		
}
