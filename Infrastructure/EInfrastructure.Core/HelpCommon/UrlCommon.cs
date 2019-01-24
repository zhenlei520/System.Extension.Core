using System.Text.RegularExpressions;
using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.HelpCommon
{
    public class UrlCommon
    {
        #region 得到url地址

        /// <summary>
        /// 得到url地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public static string GetUrl(string str)
        {
            string regexStr = "[a-zA-z]+://[^\\s]*";
            Regex reg = new Regex(regexStr, RegexOptions.Multiline);
            MatchCollection matchs = reg.Matches(str);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    return item.Value;
                }
            }

            throw new BusinessException("无效的链接");
        }

        #endregion
    }
}