using System.Linq.Expressions;

namespace Application.Expressions
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            var invoked = Expression.Invoke(exp2, exp1.Parameters.Cast<Expression>());
            return (Expression<Func<T, bool>>)Expression.Lambda(Expression.AndAlso(exp1.Body, invoked), exp1.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            var invoked = Expression.Invoke(exp2, exp1.Parameters.Cast<Expression>());
            return (Expression<Func<T, bool>>)Expression.Lambda(Expression.OrElse(exp1.Body, invoked), exp1.Parameters);
        }
    }
}
