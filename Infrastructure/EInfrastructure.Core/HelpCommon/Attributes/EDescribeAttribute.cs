using System;

namespace EInfrastructure.Core.HelpCommon.Attributes
{
    /// <summary>
    /// 描述信息
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class EDescribeAttribute
    {
        private readonly string _describe;

        public virtual string Describe => _describe;

        public EDescribeAttribute(string describe)
        {
            _describe = describe;
        }
    }
}