// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace EInfrastructure.Core.Tools.Expressions
{
    internal class ConditionBuilder : ExpressionVisitor
    {
        private List<object> m_arguments;
        private Stack<string> m_conditionParts;

        public string Condition { get; private set; }

        public object[] Arguments { get; private set; }

        public void Build(Expression expression)
        {
            PartialEvaluator evaluator = new PartialEvaluator();
            Expression evaluatedExpression = evaluator.Eval(expression);

            m_arguments = new List<object>();
            m_conditionParts = new Stack<string>();

            Visit(evaluatedExpression);

            Arguments = m_arguments.ToArray();
            Condition = m_conditionParts.Count > 0 ? m_conditionParts.Pop() : null;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b == null) return b;

            string opr;
            switch (b.NodeType)
            {
                case ExpressionType.Equal:
                    opr = "=";
                    break;
                case ExpressionType.NotEqual:
                    opr = "<>";
                    break;
                case ExpressionType.GreaterThan:
                    opr = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    opr = ">=";
                    break;
                case ExpressionType.LessThan:
                    opr = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    opr = "<=";
                    break;
                case ExpressionType.AndAlso:
                    opr = "AND";
                    break;
                case ExpressionType.OrElse:
                    opr = "OR";
                    break;
                case ExpressionType.Add:
                    opr = "+";
                    break;
                case ExpressionType.Subtract:
                    opr = "-";
                    break;
                case ExpressionType.Multiply:
                    opr = "*";
                    break;
                case ExpressionType.Divide:
                    opr = "/";
                    break;
                default:
                    throw new NotSupportedException(b.NodeType + "is not supported.");
            }

            Visit(b.Left);
            Visit(b.Right);

            string right = m_conditionParts.Pop();
            string left = m_conditionParts.Pop();

            string condition = String.Format("({0} {1} {2})", left, opr, right);
            m_conditionParts.Push(condition);

            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c == null) return c;

            m_arguments.Add(c.Value);
            m_conditionParts.Push(String.Format("{{{0}}}", m_arguments.Count - 1));

            return c;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m == null) return m;

            PropertyInfo propertyInfo = m.Member as PropertyInfo;
            if (propertyInfo == null) return m;

            m_conditionParts.Push(String.Format("[{0}]", propertyInfo.Name));

            return m;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m == null) return m;

            string format;
            switch (m.Method.Name)
            {
                case "StartsWith":
                    format = "({0} LIKE {1}+'%')";
                    break;

                case "Contains":
                    format = "({0} LIKE '%'+{1}+'%')";
                    break;

                case "EndsWith":
                    format = "({0} LIKE '%'+{1})";
                    break;

                default:
                    throw new NotSupportedException(m.NodeType + " is not supported!");
            }

            Visit(m.Object);
            Visit(m.Arguments[0]);
            string right = m_conditionParts.Pop();
            string left = m_conditionParts.Pop();
            m_conditionParts.Push(String.Format(format, left, right));

            return m;
        }
    }
}
