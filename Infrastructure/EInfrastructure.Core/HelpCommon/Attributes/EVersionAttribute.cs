using System;

namespace EInfrastructure.Core.HelpCommon.Attributes
{
    /// <summary>
    /// 版本信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EVersionAttribute : Attribute
    {
        private readonly string _version;

        public virtual string Version => _version;

        public EVersionAttribute(string version)
        {
            _version = version;
        }
    }
}