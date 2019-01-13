using System;

namespace EInfrastructure.Core.HelpCommon.Attributes
{
    /// <summary>
    /// 名称信息
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ENameAttribute : Attribute
    {
        private readonly string _name;

        public virtual string Name => _name;

        public ENameAttribute(string name)
        {
        }
    }
}