using System;

namespace EInfrastructure.Core.HelpCommon.Attributes
{
    /// <summary>
    /// 版本信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EVersionAttribute<T>
    {
        private readonly T _version;

        public virtual T Version => _version;

        public EVersionAttribute(T version)
        {
            _version = version;
        }
    }
}