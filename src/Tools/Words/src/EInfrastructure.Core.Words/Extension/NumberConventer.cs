// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Words.Extension
{
    class NumberConventer
    {
        public static Decimal ChnToArab(string chnNum)
        {
            Decimal result = 0;
            string temp = chnNum;
            bool neg = false;
            if (chnNum.IndexOf("负", StringComparison.Ordinal) != -1)
            {
                neg = true;
                temp = temp.Replace("负", string.Empty);
            }

            //string pre = string.Empty;
            //string abo = string.Empty;
            temp = temp.Replace("点", ".");
            temp = temp.Replace("分", "");
            temp = temp.Replace("角", "");
            temp = temp.Replace("元", ".");
            temp = temp.Replace("拾", "十");
            temp = temp.Replace("佰", "百");
            temp = temp.Replace("仟", "千");
            temp = temp.Replace("萬", "万");
            temp = temp.Replace("億", "亿");


            string[] part = temp.Split('.');
            if (part.Length > 1) result = GetArabDotPart(part[1]);


            string[] sp = part[0].Split(new char[] {'亿'}, StringSplitOptions.RemoveEmptyEntries);
            if (sp.Length == 3)
            {
                result +=
                    ((HandlePart(sp[0]) * 10000000000000000) + (HandlePart(sp[1]) * 100000000) + HandlePart(sp[2]));
            }
            else if (sp.Length == 2)
            {
                result += ((HandlePart(sp[0]) * 100000000) + HandlePart(sp[1]));
            }
            else if (sp.Length == 1)
            {
                result += HandlePart(sp[0]);
            }

            if (neg) return -result;
            return result;
        }

        /// <summary>
        /// 处理亿以下内容。
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static Decimal HandlePart(string num)
        {
            Decimal result = 0;
            string[] part = num.Split(new char[] {'万'}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < part.Length; i++)
            {
                result += Convert.ToDecimal(GetArabThousandPart(part[part.Length - i - 1]))
                          * Convert.ToDecimal((System.Math.Pow(10000, Convert.ToDouble(i))));
            }

            return result;
        }

        /// <summary>
        /// 取得阿拉伯数字小数部分。
        /// </summary>
        /// <returns></returns>
        private static Decimal GetArabDotPart(string dotpart)
        {
            Decimal result = 0.00M;
            string spe = "0.";
            for (int i = 0; i < dotpart.Length; i++)
            {
                spe += switchNum(dotpart[i].ToString()).ToString();
            }

            result = Convert.ToDecimal(spe);
            return result;
        }

        private static int GetArabThousandPart(string number)
        {
            string chnNumString = number;
            if (chnNumString == "零")
            {
                return 0;
            }

            if (chnNumString != string.Empty)
            {
                if (chnNumString[0].ToString() == "十")
                {
                    chnNumString = "一" + chnNumString;
                }
            }

            chnNumString = chnNumString.Replace("零", string.Empty);
            //去除所有的零
            int result = 0;
            int index = chnNumString.IndexOf("千", StringComparison.Ordinal);
            if (index != -1)
            {
                result += switchNum(chnNumString.Substring(0, index)) * 1000;
                chnNumString = chnNumString.Remove(0, index + 1);
            }

            index = chnNumString.IndexOf("百", StringComparison.Ordinal);
            if (index != -1)
            {
                result += switchNum(chnNumString.Substring(0, index)) * 100;
                chnNumString = chnNumString.Remove(0, index + 1);
            }

            index = chnNumString.IndexOf("十", StringComparison.Ordinal);
            if (index != -1)
            {
                result += switchNum(chnNumString.Substring(0, index)) * 10;
                chnNumString = chnNumString.Remove(0, index + 1);
            }

            if (chnNumString != string.Empty)
            {
                result += switchNum(chnNumString);
            }

            return result;
        }

        /// <summary>
        /// 取得汉字对应的阿拉伯数字
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int switchNum(string n)
        {
            switch (n)
            {
                case "壹": return 1;
                case "贰": return 2;
                case "叁": return 3;
                case "肆": return 4;
                case "伍": return 5;
                case "陆": return 6;
                case "柒": return 7;
                case "捌": return 8;
                case "玖": return 9;

                case "零": return 0;
                case "一": return 1;
                case "二": return 2;
                case "两": return 2;
                case "三": return 3;
                case "四": return 4;
                case "五": return 5;
                case "六": return 6;
                case "七": return 7;
                case "八": return 8;
                case "九": return 9;

                default: return -1;
            }
        }
    }
}