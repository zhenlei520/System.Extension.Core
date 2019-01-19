using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common
{
    public class PinYinWords
    {
        public string Words { get; set; }
        public string PinYins { get; set; }

        private List<string> PinYinList { get; set; }

        public int[] GetPinYinIndex()
        {
            PinYinList = PinYins.Split('\'').ToList();
            int[] pys = new int[Words.Length];
            for (int i = 0; i < Words.Length; i++)
            {
                pys[i] = GetPyName(PinYinList[i]);
            }

            return pys;
        }

        #region GetPyName

        /// <summary>
        /// 得到拼音名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetPyName(string name)
        {
            name = name.Replace("0", "").Replace("1", "").Replace("2", "").Replace("3", "").Replace("4", "")
                .Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "").Replace("9", "").ToUpper();
            if (name.Length > 1)
            {
                name = name[0] + name.Substring(1).ToLower();
            }

            return BaseWordService.DictPinYinConfig.PinYinName.IndexOf(name);
        }

        #endregion

        public override int GetHashCode()
        {
            return Words.GetHashCode();
        }

        public override string ToString()
        {
            return Words + "|" + PinYins;
        }
    }
}