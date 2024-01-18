using AutoMapper;
using BusinessLogic.Contracts;
using Contracts;
using Entities;
using Entities.Exceptions;
using Shared.DataTransferObjects;

namespace BusinessLogic.Services
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;

        private readonly ILoggerManager _loggerManager;

        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            _ = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges) ??
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

            _repositoryManager.EmployeeRepository.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repositoryManager.Save();

            var entityToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return entityToReturn;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        {
            _ = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges) ??
                throw new CompanyNotFoundException(companyId);
            var employee = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, employeeId, trackChanges);
            return employee is null ? throw new EmployeeNotFoundException(employeeId) : _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employees = await _repositoryManager.EmployeeRepository.GetEmployeesAsync(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, id, trackChanges);
            if (employeeForCompany is null)
                throw new EmployeeNotFoundException(id);

            _repositoryManager.EmployeeRepository.DeleteEmployee(employeeForCompany);
            await _repositoryManager.Save();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, EmployeeForUpdateDto employeeForUpdate, Guid id, bool compTrackChange, bool empTrackChanges)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, compTrackChange);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, id, empTrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdate, employeeEntity);
            await _repositoryManager.Save();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatch(
            Guid companyId, Guid id, bool comTrackChange, bool empTrackChange)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, comTrackChange);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, id, empTrackChange);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repositoryManager.Save();
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool comTrackChange, bool empTrackChanges)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(companyId, comTrackChange) ?? throw new CompanyNotFoundException(companyId);
            var employee = await _repositoryManager.EmployeeRepository.GetEmployeeAsync(companyId, id, empTrackChanges) ?? throw new EmployeeNotFoundException(companyId);
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employee);
            return (employeeToPatch, employee);

        }
    }
}
