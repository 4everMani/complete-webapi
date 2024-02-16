using System.Reflection;
using System.Text;

namespace Repository.Extensions
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQuerying)
        {
            var orderParams = orderByQuerying.Trim().Split(',');
            var properyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach(string param in  orderParams)
            {
                if (string.IsNullOrWhiteSpace(param)) continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = properyInfos.FirstOrDefault(pi => 
                pi.Name.Equals(propertyFromQueryName, StringComparison.OrdinalIgnoreCase));

                if (objectProperty is null) continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return orderQuery;
        }
    }
}
