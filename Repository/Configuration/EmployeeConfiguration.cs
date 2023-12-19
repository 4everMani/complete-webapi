using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
                (
                    new Employee
                    {
                        Id = new Guid("929ab1b5-16a0-45e0-81ec-c621d363ebbf"),
                        Name = "Manish Jaiswal",
                        Age = 24,
                        CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Position = "Senior Engineer",
                    },
                    new Employee
                    {
                        Id = new Guid("616222b0-e9e3-47df-887b-d78476410fac"),
                        Name = "Sourya Gupta",
                        Age = 25,
                        CompanyId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                        Position = "Junior Developer",
                    },
                    new Employee
                    {
                        Id = new Guid("1703d193-34a4-40ea-9423-bc8452e158d1"),
                        Name = "Nikhil Mishra",
                        Age = 23,
                        CompanyId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                        Position = "Intern",
                    }
                );
        }
    }
}
