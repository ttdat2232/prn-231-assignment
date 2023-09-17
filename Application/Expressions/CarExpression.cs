using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Expressions
{
    public static class CarExpression
    {
        public static Expression<Func<CarInformation, bool>> CarContainsName(string? name)
        {
            if (name == null || !name.Any())
                return c => true;
            return c => c.CarName.ToLower().Contains(name.ToLower().Trim());
        }
        
        public static Expression<Func<CarInformation, bool>> CarInStatuses(int[]? statuses)
        {
            if(statuses == null || statuses.Length == 0)
                return c => true;
            Expression<Func<CarInformation, bool>> exp = c => false;
            foreach(var stat in statuses)
                exp = PredicateBuilder.Or(exp, c => c.CarStatus == stat);
            return exp;
        }
    }
}
