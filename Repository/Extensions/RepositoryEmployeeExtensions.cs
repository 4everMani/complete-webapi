﻿using Entities;

namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployee(this IQueryable<Employee> employees, uint minAge, uint maxAge) =>
            employees.Where(e => (e.Age <= maxAge && e.Age >= minAge));


        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employees;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}