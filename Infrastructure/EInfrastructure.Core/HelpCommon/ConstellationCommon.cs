using System;
using EInfrastructure.Core.HelpCommon.Enums;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 星座帮助类
    /// </summary>
    public class ConstellationHelper
    {
        #region 根据日期得到星座名称
        /// <summary>
        /// 根据日期得到星座名称
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <returns></returns>
        public static string GetConstellationFromBirthday(DateTime birthday)
        {
            float fBirthDay = Convert.ToSingle(birthday.ToString("M.dd"));
            float[] atomBound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };
            string[] atoms = { "水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "魔羯座" };
            string ret = "";
            for (int i = 0; i < atomBound.Length - 1; i++)
            {
                if (atomBound[i] <= fBirthDay && atomBound[i + 1] > fBirthDay)
                {
                    ret = atoms[i];
                    break;
                }
            }
            return ret;
        }
        #endregion

        #region 根据日期得到星座枚举
        /// <summary>
        /// 根据日期得到星座枚举
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <returns></returns>
        public static ConstellationEnum GetConstellationEnumFromBirthday(DateTime birthday)
        {
            var name = GetConstellationFromBirthday(birthday);
            switch (name)
            {
                case "水瓶座":
                    return ConstellationEnum.Aquarius;
                case "双鱼座":
                    return ConstellationEnum.Pisces;
                case "白羊座":
                    return ConstellationEnum.Aries;
                case "金牛座":
                    return ConstellationEnum.Taurus;
                case "双子座":
                    return ConstellationEnum.Gemini;
                case "巨蟹座":
                    return ConstellationEnum.Cancer;
                case "狮子座":
                    return ConstellationEnum.Leo;
                case "处女座":
                    return ConstellationEnum.Virgo;
                case "天秤座":
                    return ConstellationEnum.Libra;
                case "天蝎座":
                    return ConstellationEnum.Scorpio;
                case "射手座":
                    return ConstellationEnum.Sagittarius;
                case "魔羯座":
                    return ConstellationEnum.Capricornus;
                default:
                    return 0;
            }
        } 
        #endregion
    }
}