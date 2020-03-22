using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 性别帮助类
    /// </summary>
    public static class GenderCommon
    {
        #region 根据身份证号码获取性别

        /// <summary>
        /// 根据身份证号码获取性别
        /// </summary>
        /// <param name="cardNo">身份证号码</param>
        /// <returns></returns>
        public static Gender GetGender(this string cardNo)
        {
            if (!cardNo.IsIdCard())
            {
                throw new BusinessException("请输入合法的身份证号码");
            }

            int? gender = (cardNo.Length == 15 ? cardNo.Substring(14, 1) : cardNo.Substring(16, 1)).ConvertToInt(null);
            if (gender == null)
            {
                return null;
            }

            return gender % 2 == 0 ? Gender.Girl : Gender.Boy;
        }

        #endregion
    }
}
