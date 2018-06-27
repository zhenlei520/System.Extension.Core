using System.Text.RegularExpressions;
using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.HelpCommon
{
    public class Check
    {
        public static void NotNull(object obj, string message = "")
        {
            if (ReferenceEquals(obj, null))
            {
                if (string.IsNullOrEmpty(message))
                {
                    message = obj.ToString() + "空引用";
                }

                throw new System.Exception(message);
            }
        }

        /// <summary>
        /// 判断是否是淘口令
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAmoyPsd(string str, ref string code)
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

        public static bool IsUrl(string str)
        {
            return Regex.IsMatch(str??"", @"^(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]$");
        }

        public static string GetUrl(string str)
        {
            string regexStr = "[a-zA-Z]+://[^\\s]*";
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
            return "";
        }
    }
}