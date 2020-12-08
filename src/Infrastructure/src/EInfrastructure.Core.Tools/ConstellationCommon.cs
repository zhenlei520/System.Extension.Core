// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 星座帮助类
    /// </summary>
    public static class ConstellationCommon
    {
        #region 根据身份证号码得到星座信息

        /// <summary>
        /// 根据身份证号码得到星座信息
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <returns></returns>
        public static Constellation GetConstellationFromCardNo(this string cardNo)
        {
            return ObjectCommon.SafeObject<Constellation>(!cardNo.IsNullOrWhiteSpace(),
                () => (cardNo.GetBirthday().GetConstellationFromBirthday(), Constellation.Unknow));
        }

        #endregion
    }
}
