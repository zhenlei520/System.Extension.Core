using System;
using System.Text.RegularExpressions;

namespace EInfrastructure.Core.HelpCommon
{
    public static class Check
    {
        #region 检查空或者null

        /// <summary>
        /// 检查空或者null
        /// </summary>
        /// <param name="str"></param>
        /// <param name="message"></param>
        /// <param name="action"></param>
        public static void IsNullOrEmptyTip(this string str, string message, Func<bool> action = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (action == null || action.Invoke())
                {
                    throw new System.Exception(message);
                }
            }
        }

        #endregion

        #region 检查是否空或者null

        /// <summary>
        /// 检查是否空或者null
        /// </summary>
        /// <param name="array"></param>
        /// <param name="message"></param>
        /// <param name="action"></param>
        /// <exception cref="Exception"></exception>
        public static void IsNullOrEmptyTip(this object[] array, string message, Func<bool> action = null)
        {
            if (array == null || array.Length == 0)
            {
                if (action == null || action.Invoke())
                {
                    throw new System.Exception(message);
                }
            }
        }

        #endregion

        #region 判断是否是淘口令

        /// <summary>
        /// 判断是否是淘口令
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAmoyPsdTip(string str, ref string code)
        {
            Regex reg = new Regex("(￥|€|《).*(￥|€|《)", RegexOptions.Multiline);
            MatchCollection matchs = reg.Matches(str);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    code = item.Value;
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}