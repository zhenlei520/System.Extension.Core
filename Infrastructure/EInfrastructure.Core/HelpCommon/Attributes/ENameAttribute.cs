using System;

namespace EInfrastructure.Core.HelpCommon.Attributes
{
    /// <summary>
    /// 名称信息
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ENameAttribute : Attribute
    {
        /// <summary>
        /// 自定义名称属性
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// 自定义名称属性
        /// </summary>
        public virtual string Name => _name;

        /// <summary>
        /// 名称信息
        /// </summary>
        /// <param name="name"></param>
        public ENameAttribute(string name)
        {
            _name = name;
        }
    }
}