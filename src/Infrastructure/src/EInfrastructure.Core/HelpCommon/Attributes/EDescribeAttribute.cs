using System;

namespace EInfrastructure.Core.HelpCommon.Attributes
{
    /// <summary>
    /// 描述信息
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class EDescribeAttribute : Attribute
    {
        private readonly string _describe;

        /// <summary>
        /// 
        /// </summary>
        public virtual string Describe => _describe;

        /// <summary>
        /// 自定义描述信息
        /// </summary>
        /// <param name="describe"></param>
        public EDescribeAttribute(string describe)
        {
            _describe = describe;
        }
    }
}