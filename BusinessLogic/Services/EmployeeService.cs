using AutoMapper;
using BusinessLogic.Contracts;
using Contracts;
using Entities;
using Entities.Exceptions;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly ILoggerManager _loggerManager;

        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper )
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            _ = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges) ?? throw new CompanyNotFoundException(companyId);
            var employee = _repositoryManager.EmployeeRepository.GetEmployee(companyId, employeeId, trackChanges);
            return employee is null ? throw new EmployeeNotFoundException(employeeId) : _mapper.Map<EmployeeDto>(employee);
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employees = _repositoryManager.EmployeeRepository.GetEmployees(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }
    }
}
