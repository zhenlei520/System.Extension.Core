// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EInfrastructure.Core.Config.Entities.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class PartialEvaluator : ExpressionVisitor
    {
        private Func<Expression, bool> m_fnCanBeEvaluated;
        private HashSet<Expression> m_candidates;

        /// <summary>
        /// 
        /// </summary>
        public PartialEvaluator()
            : this(CanBeEvaluatedLocally)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fnCanBeEvaluated"></param>
        public PartialEvaluator(Func<Expression, bool> fnCanBeEvaluated)
        {
            m_fnCanBeEvaluated = fnCanBeEvaluated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Expression Eval(Expression exp)
        {
            m_candidates = new Nominator(m_fnCanBeEvaluated).Nominate(exp);

            return Visit(exp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected override Expression Visit(Expression exp)
        {
            if (exp == null)
            {
                return null;
            }

            if (m_candidates.Contains(exp))
            {
                return Evaluate(exp);
            }

            return base.Visit(exp);
        }

        private Expression Evaluate(Expression e)
        {
            if (e.NodeType == ExpressionType.Constant)
            {
                return e;
            }

            LambdaExpression lambda = Expression.Lambda(e);
            Delegate fn = lambda.Compile();

            return Expression.Constant(fn.DynamicInvoke(null), e.Type);
        }

        private static bool CanBeEvaluatedLocally(Expression exp)
        {
            return exp.NodeType != ExpressionType.Parameter;
        }

        #region Nominator

        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly
        /// be part of an evaluated sub-tree.
        /// </summary>
        private class Nominator : ExpressionVisitor
        {
            private Func<Expression, bool> m_fnCanBeEvaluated;
            private HashSet<Expression> m_candidates;
            private bool m_cannotBeEvaluated;

            internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                m_fnCanBeEvaluated = fnCanBeEvaluated;
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                m_candidates = new HashSet<Expression>();
                Visit(expression);
                return m_candidates;
            }

            protected override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    bool saveCannotBeEvaluated = m_cannotBeEvaluated;
                    m_cannotBeEvaluated = false;

                    base.Visit(expression);

                    if (!m_cannotBeEvaluated)
                    {
                        if (m_fnCanBeEvaluated(expression))
                        {
                            m_candidates.Add(expression);
                        }
                        else
                        {
                            m_cannotBeEvaluated = true;
                        }
                    }

                    m_cannotBeEvaluated |= saveCannotBeEvaluated;
                }

                return expression;
            }
        }

        #endregion
    }
}
