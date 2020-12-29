// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 身份证帮助类
    /// </summary>
    public static class IdCardCommon
    {
        #region 根据身份证号得到生肖信息 如果身份证号码错误，则返回Null

        /// <summary>
        /// 根据身份证号得到生肖信息 如果身份证号码错误，则返回Null
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <returns></returns>
        public static Animal GetAnimal(string cardNo)
        {
            if (!cardNo.IsIdCard())
            {
                return null;
            }

            var birthday = GetBirthday(cardNo);
            return birthday != null ? TimeCommon.GetAnimal(birthday.Value.Year) : null;
        }

        #endregion

        #region 根据身份证号码获取出生日期

        /// <summary>
        /// 根据身份证号码获取出生日期
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static DateTime? GetBirthday(string cardNo, Func<DateTime?> func = null)
        {
            if (!cardNo.IsIdCard())
            {
                throw new BusinessException("请输入合法的身份证号码", HttpStatus.Err.Id);
            }

            string timeStr = cardNo.Length == 15
                ? ("19" + cardNo.Substring(6, 2)) + "-" + cardNo.Substring(8, 2) + "-" +
                  cardNo.Substring(10, 2)
                : cardNo.Substring(6, 4) + "-" + cardNo.Substring(10, 2) + "-" + cardNo.Substring(12, 2);
            return timeStr.ConvertToDateTime(func?.Invoke());
        }

        #endregion

        #region 根据身份证号码获取性别

        /// <summary>
        /// 根据身份证号码获取性别
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <param name="action">查询性别失败转换，默认为未知</param>
        /// <returns></returns>
        public static Gender GetGender(string cardNo, Func<Gender> action = null)
        {
            if (!cardNo.IsIdCard())
            {
                return action?.Invoke() ?? Gender.Unknow;
            }

            int? gender = (cardNo.Length == 15 ? cardNo.Substring(14, 1) : cardNo.Substring(16, 1)).ConvertToInt(null);
            if (gender == null)
            {
                return action?.Invoke() ?? Gender.Unknow;
            }

            return gender % 2 == 0 ? Gender.Girl : Gender.Boy;
        }

        #endregion

        #region 根据身份证号码得到星座信息

        /// <summary>
        /// 根据身份证号码得到星座信息
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <returns></returns>
        public static Constellation GetConstellation(string cardNo)
        {
            if (cardNo.IsIdCard())
            {
                return ObjectCommon.SafeObject<Constellation>(!cardNo.IsNullOrWhiteSpace(),
                    () => GetBirthday(cardNo).GetConstellationFromBirthday(), () => Constellation.Unknow);
            }

            return Constellation.Unknow;
        }

        #endregion
    }
}
