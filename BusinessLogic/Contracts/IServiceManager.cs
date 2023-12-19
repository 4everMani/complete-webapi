namespace BusinessLogic.Contracts
{
    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }

        IEmployeeService EmployeeService { get; }
    }
}
