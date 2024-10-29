using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLib.Extension
{
    public static class ExpressionExtend
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
            {
                return expr2;
            }

            if (expr2 == null)
            {
                return expr1;
            }

            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor newExpressionVisitor = new NewExpressionVisitor(parameterExpression);
            Expression left = newExpressionVisitor.Replace(expr1.Body);
            Expression right = newExpressionVisitor.Replace(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.And(left, right), new ParameterExpression[1] { parameterExpression });
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
            {
                return expr2;
            }

            if (expr2 == null)
            {
                return expr1;
            }

            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "c");
            NewExpressionVisitor newExpressionVisitor = new NewExpressionVisitor(parameterExpression);
            Expression left = newExpressionVisitor.Replace(expr1.Body);
            Expression right = newExpressionVisitor.Replace(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.Or(left, right), new ParameterExpression[1] { parameterExpression });
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            if (expr == null)
            {
                return null;
            }

            ParameterExpression parameterExpression = expr.Parameters[0];
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), new ParameterExpression[1] { parameterExpression });
        }

        public static Expression<Func<T, bool>> ToAndExpression<T>(this IEnumerable<Expression<Func<T, bool>>> exprs)
        {
            Expression<Func<T, bool>> result = null;
            List<Expression<Func<T, bool>>> list = exprs.ToList();
            if (list.Count > 1)
            {
                for (int i = 0; i < list.Count && i + 1 < list.Count; i++)
                {
                    result = list[i].And(list[i + 1]);
                }
            }
            else
            {
                result = ((list.Count <= 0) ? ((Expression<Func<T, bool>>)((T o) => true)) : exprs.FirstOrDefault());
            }

            return result;
        }
    }
}
