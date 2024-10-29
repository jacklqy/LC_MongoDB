using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLib.Extension
{
    internal class NewExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _NewParameter { get; set; }

        public NewExpressionVisitor(ParameterExpression param)
        {
            _NewParameter = param;
        }

        public Expression Replace(Expression exp)
        {
            return Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _NewParameter;
        }
    }
}
