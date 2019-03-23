using System;
using EInfrastructure.Core.HelpCommon;
using Xunit;

namespace EInfrastructure.Core.UnitTest
{
    /// <summary>
    /// 安全方法测试
    /// </summary>
    public class SecurityCommonUnitTest
    {
        /// <summary>
        /// Aes
        /// </summary>
        [Fact]
        public void AesEncrypt()
        {
            string aesKey = "Aes密钥";
            string param = "待解密的参数";
            string tokenStr = UrlCommon.UrlDecode(param);
            var result = SecurityCommon.Decrypt(tokenStr,
                aesKey);
            Console.WriteLine($"result:{result}");
        }
    }
}