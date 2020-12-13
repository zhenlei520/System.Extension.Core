// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace EInfrastructure.Core.Tools.Expressions
{
    /// <summary>
    /// 条件表达式
    /// </summary>
    internal class ConditionBuilder : ExpressionVisitor
    {
        private List<object> _mArguments;
        private Stack<string> _mConditionParts;

        public string Condition { get; private set; }

        public object[] Arguments { get; private set; }

        public void Build(Expression expression)
        {
            PartialEvaluator evaluator = new PartialEvaluator();
            Expression evaluatedExpression = evaluator.Eval(expression);

            _mArguments = new List<object>();
            _mConditionParts = new Stack<string>();

            Visit(evaluatedExpression);

            Arguments = _mArguments.ToArray();
            Condition = _mConditionParts.Count > 0 ? _mConditionParts.Pop() : null;
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

            string right = _mConditionParts.Pop();
            string left = _mConditionParts.Pop();

            string condition = $"({left} {opr} {right})";
            _mConditionParts.Push(condition);

            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c == null) return c;

            _mArguments.Add(c.Value);
            _mConditionParts.Push(String.Format("{{{0}}}", _mArguments.Count - 1));

            return c;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m == null) return m;

            PropertyInfo propertyInfo = m.Member as PropertyInfo;
            if (propertyInfo == null) return m;

            _mConditionParts.Push(String.Format("[{0}]", propertyInfo.Name));

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
            string right = _mConditionParts.Pop();
            string left = _mConditionParts.Pop();
            _mConditionParts.Push(String.Format(format, left, right));

            return m;
        }
    }
}
