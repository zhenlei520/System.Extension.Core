using System.Collections.Generic;
using System.Linq;
using System.Text;
using EInfrastructure.Core.AspNetCore.AutoConfig.Extension;
using EInfrastructure.Core.Configuration.Interface.Config;
using EInfrastructure.Core.Interface.Words;
using EInfrastructure.Core.Interface.Words.Config;
using EInfrastructure.Core.Interface.Words.Enum;

namespace EInfrastructure.Core.Words
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseWordService
    {
        public BaseWordService(IWritableOptions<object> options)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        #region protected methods

        #endregion
    }
}