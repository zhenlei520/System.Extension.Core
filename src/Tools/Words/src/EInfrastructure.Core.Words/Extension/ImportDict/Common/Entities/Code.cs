using System.Collections.Generic;
using EInfrastructure.Core.Words.Extension.ImportDict.Common.Enum;
using EInfrastructure.Core.Words.Extension.ImportDict.Common.Helpers;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common.Entities
{
   internal class Code:List<IList<string>>
    {
        internal Code(IEnumerable<IList<string>> code)
        {
            foreach (var c in code)
            {
                this.Add(c);
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="code"></param>
       /// <param name="is1Char1Code">是否是单拼音这样的一字一码，如果不是则表示为一词多码</param>
        public Code(IList<string> code,bool is1Char1Code)
        {
            if (is1Char1Code)
            {
                foreach (var py in code)
                {
                    this.Add(new List<string> {py});
                }
            }
            else
            {
                this.Add(code);
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="code">五笔这种一词一码类型</param>
        public Code(string code)
        {
            this.Add(new List<string>(){code});
        }

       public Code()
       {
           
       }
       /// <summary>
       /// 取得每个字的编码的第一个编码
       /// </summary>
       /// <returns></returns>
       public IList<string> GetDefaultCode()
       {
           var result = new List<string>();
           foreach (var row in this)
           {
               result.Add(row[0]);
           }
           return result;
       }

       public IList<string> ToCodeString(string codeSplit="", BuildTypeEnum buildType=BuildTypeEnum.None)
       {
          return CollectionHelper.CartesianProduct(this, codeSplit, buildType);
       }

       /// <summary>
       /// 取得第一行第一列编码
       /// </summary>
       /// <returns></returns>
       public string GetTop1Code()
       {
           return this[0][0];
       }
    }
}
