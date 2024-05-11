namespace Shared.DataTransferObjects;

public record CompanyForUpdateDto(string name, string address, string country,
    IEnumerable<EmployeeForCreationDto> employee);

