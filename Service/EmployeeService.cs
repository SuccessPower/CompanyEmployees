using AutoMapper;
using Contracts;
using Entities.Entities;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

				public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
				{
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
				}

				public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges)
				{
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            var employeeDb = _repository.Employee.getEmployee(companyId, id, trackChanges);
            if(employeeDb == null)
                throw new EmployeeNotFoundException(companyId);

            var employee = _mapper.Map<EmployeeDto>(employeeDb);

            return employee;

				}

				public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
				{
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null) 
                throw new CompanyNotFoundException(companyId);

            var employeesFromDb = _repository.Employee.GetEmployees(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return employeesDto;
				}


		}
}
