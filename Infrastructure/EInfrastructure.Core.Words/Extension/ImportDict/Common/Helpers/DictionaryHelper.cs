using System;
using System.Collections.Generic;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common.Helpers
{
    internal class DictionaryHelper
    {
        private static readonly Dictionary<char, ChineseCode> Dictionary = new Dictionary<char, ChineseCode>();

        private static Dictionary<char, ChineseCode> Dict
        {
            get
            {
                if (Dictionary.Count == 0)
                {
                    string allPinYin = ""; // Dictionaries.ChineseCode;
                    string[] pyList = allPinYin.Split(new[] {"\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < pyList.Length; i++)
                    {
                        string[] hzpy = pyList[i].Split('\t');
                        char hz = Convert.ToChar(hzpy[1]);

                        Dictionary.Add(hz, new ChineseCode
                        {
                            Code = hzpy[0],
                            Word = hzpy[1][0],
                            Wubi86 = hzpy[2],
                            Wubi98 = (hzpy[3] == "" ? hzpy[2] : hzpy[3]),
                            Pinyins = hzpy[4],
                            Freq = Convert.ToDouble(hzpy[5])
                        });
                    }
                }

                return Dictionary;
            }
        }

        public static ChineseCode GetCode(char c)
        {
            return Dict[c];
        }


        public static List<ChineseCode> GetAll()
        {
            return new List<ChineseCode>(Dict.Values);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public struct ChineseCode
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public char Word { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Wubi86 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Wubi98 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pinyins { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Freq { get; set; }
    }
}