using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Schedule.io.Extensions
{
    //Attributed to: Adam Tegen via StackOverflow http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
    //Modified by Ed Charbeneau
    public static class PredicadoExtensions
    {
        private static string DEFAULT_VALUE = "parameter";
        /// <summary>
        /// Inicia uma cadeia de expressão.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Valor de retorno padrão se a cadeia de expressão for encerrado de forma inexperada.</param>
        /// <returns>A lambda expression stub</returns>
        public static Expression<Func<T, bool>> Iniciar<T>(bool value = false)
        {
            if (value)
                return parameter => true; //value cannot be used in place of true/false

            return parameter => false;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            return CombineLambdas(left, right, ExpressionType.AndAlso);
        }

        public static bool PossuiParametro<T>(this Expression<Func<T, bool>> expression)
        {
            if (expression == null)
                return false;

            if (expression.Parameters.FirstOrDefault().Name.ToLower() == DEFAULT_VALUE.ToLower())
                return false;

            return true;
        }


        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return CombineLambdas(left, right, ExpressionType.OrElse);
        }

        #region private

        private static Expression<Func<T, bool>> CombineLambdas<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right, ExpressionType expressionType)
        {
            //Remove expressions created with Begin<T>()
            if (IsExpressionBodyConstant(left))
                return (right);

            ParameterExpression p = left.Parameters[0];

            SubstituteParameterVisitor visitor = new SubstituteParameterVisitor();
            visitor.Sub[right.Parameters[0]] = p;

            Expression body = Expression.MakeBinary(expressionType, left.Body, visitor.Visit(right.Body));

            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        private static bool IsExpressionBodyConstant<T>(Expression<Func<T, bool>> left)
        {
            return left.Body.NodeType == ExpressionType.Constant;
        }

        internal class SubstituteParameterVisitor : ExpressionVisitor
        {
            public Dictionary<Expression, Expression> Sub = new Dictionary<Expression, Expression>();

            protected override Expression VisitParameter(ParameterExpression node)
            {
                Expression newValue;
                if (Sub.TryGetValue(node, out newValue))
                {
                    return newValue;
                }
                return node;
            }
        }

        #endregion
    }
}