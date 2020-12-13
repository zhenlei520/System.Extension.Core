// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 拷贝
    /// </summary>
    internal class CloneableCommon<TSource, TOpt>
        where TSource : class, new()
        where TOpt : class, new()
    {
        /// <summary>
        ///
        /// </summary>
        private static readonly List<string> CopyableTypes = new List<string> {"System.String"};

        /// <summary>
        ///
        /// </summary>
        private static Func<TSource, TOpt> _cache = null;

        /// <summary>
        ///
        /// </summary>
        private static ParameterExpression _inputParameterExpression;

        /// <summary>
        /// 转换方法
        /// </summary>
        /// <returns>目标实体</returns>
        private static TOpt GetFunc(TSource instance)
        {
            if (_cache != null)
                return _cache(instance);
            MemberInitExpression memberInitExpression = GetMemberInitExp(instance);
            Expression<Func<TSource, TOpt>> lambda =
                Expression.Lambda<Func<TSource, TOpt>>(memberInitExpression, _inputParameterExpression);
            _cache = lambda.Compile();
            return _cache(instance);
        }

        /// <summary>
        /// 得到完整命名空间
        /// </summary>
        /// <returns></returns>
        private static string GetFullName()
        {
            return (typeof(TSource).FullName?.Replace(".", "") ?? "");
        }

        /// <summary>
        /// 转换方法
        /// </summary>
        /// <returns>目标实体</returns>
        private static MemberInitExpression GetMemberInitExp(TSource instance)
        {
            var typeIn = typeof(TSource);
            var typeOut = typeof(TOpt);

            _inputParameterExpression = Expression.Parameter(typeof(TSource), GetFullName());

            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (var outItem in typeOut.GetProperties(bindingFlags))
            {
                if (!outItem.CanWrite) continue;
                var inProperty = typeIn.GetProperty(outItem.Name, bindingFlags);
                if (inProperty == null) continue;

                if (inProperty.PropertyType != outItem.PropertyType)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"注意★ 模型映射时,类型 {typeIn.FullName} 的属性:{outItem.Name},虽然名称一致但类型不匹配,跳过给此属性赋相应值");
                    continue;
                }

                if (inProperty.PropertyType.IsValueType || CopyableTypes.Any(x =>
                    x.Equals(inProperty.PropertyType.FullName, StringComparison.OrdinalIgnoreCase)))
                {
                    MemberExpression property = Expression.Property(_inputParameterExpression,
                        inProperty);
                    MemberBinding memberBinding = Expression.Bind(outItem, property);
                    memberBindingList.Add(memberBinding);
                }
                else //复杂对象
                {
                    var methodInfo = typeof(CloneableCommon<,>)
                        .MakeGenericType(inProperty.PropertyType, outItem.PropertyType)
                        .GetMethod("GetFunc", BindingFlags.NonPublic | BindingFlags.Static);
                    if (methodInfo == null)
                    {
                        throw new ArgumentNullException(nameof(methodInfo));
                    }
                    var val = inProperty.GetValue(instance);
                    var oVar = Expression.Constant(val, inProperty.PropertyType);
                    var call = Expression.Call(methodInfo, oVar);
                    MemberBinding memberBinding = Expression.Bind(outItem, call);
                    memberBindingList.Add(memberBinding);
                }
            }

            MemberInitExpression memberInitExpression =
                Expression.MemberInit(Expression.New(typeOut), memberBindingList.ToArray());
            return memberInitExpression;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="tIn">源实体</param>
        /// <param name="optAction">失败回调</param>
        /// <returns>目标实体</returns>
        public static TOpt DeepCopy(TSource tIn, Func<Exception, TOpt> optAction = null)
        {
            try
            {
                return GetFunc(tIn);
            }
            catch (Exception ex)
            {
                return optAction?.Invoke(ex) ?? default(TOpt);
            }
        }
    }

    /// <summary>
    /// 拷贝
    /// </summary>
    public static class CloneableCommon
    {
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="optAction">失败回调</param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static TSource DeepCopy<TSource>(this TSource instance, Func<Exception, TSource> optAction = null)
            where TSource : class, new()
        {
            return CloneableCommon<TSource, TSource>.DeepCopy(instance, optAction);
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="optAction">失败回调</param>
        /// <typeparam name="TSource">源类</typeparam>
        /// <typeparam name="TOpt">目标类</typeparam>
        /// <returns></returns>
        public static TOpt DeepCopy<TSource, TOpt>(this TSource instance, Func<Exception, TOpt> optAction = null)
            where TSource : class, new()
            where TOpt : class, new()
        {
            return CloneableCommon<TSource, TOpt>.DeepCopy(instance, optAction);
        }
    }
}
