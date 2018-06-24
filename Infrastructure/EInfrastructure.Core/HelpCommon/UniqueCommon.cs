using System;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 唯一方法实现
    /// </summary>
    public class UniqueCommon
    {
        #region 全局唯一Guid
        /// <summary>
        /// 全局唯一Guid
        /// </summary>
        public static string Guids => Guid.NewGuid().ToString().Replace("-", "");

        #endregion
    }
}

