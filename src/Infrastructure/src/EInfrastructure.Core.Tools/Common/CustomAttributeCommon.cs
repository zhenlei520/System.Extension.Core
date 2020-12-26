// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 自定义属性
    /// CustomAttributeCommon.GetCustomAttribute<ENameAttribute, string>(typeof(Test), x => x.Name)
    /// </summary>
    /// 其中Test为用户自定义类
    public static class CustomAttributeCommon
    {
        #region 获取自定义属性的值

        /// <summary>
        /// 获取自定义属性的值
        /// </summary>
        /// <param name="sourceType">头部标有CustomAttribute类的类型</param>
        /// <param name="attributeValueAction">取Attribute具体哪个属性值的匿名函数</param>
        /// <param name="name">field name或property name</param>
        /// <returns>返回Attribute的值，没有则返回null</returns>
        public static TSource GetCustomAttribute<T, TSource>(Type sourceType,
            Func<T, TSource> attributeValueAction,
            string name = null)
            where T : Attribute
            where TSource : IComparable
        {
            return Configuration.CustomAttribute<T, TSource>.GetAttributeValue(sourceType,
                attributeValueAction, name);
        }

        #endregion
    }
}
