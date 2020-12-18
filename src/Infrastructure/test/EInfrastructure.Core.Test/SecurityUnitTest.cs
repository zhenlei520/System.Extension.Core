// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Common;
using Xunit;

namespace EInfrastructure.Core.Test
{
    public class SecurityUnitTest : BaseUnitTest
    {
        public SecurityUnitTest() : base()
        {
        }

        /// <summary>
        ///
        /// </summary>
        private string Key = "12345678912345678912345678912345";

        private  byte[] Iv= { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        ///
        /// </summary>
        [Theory]
        [InlineData("123")]
        public void Aes(string str)
        {
            var str1 = str.AesEncrypt(Key);
            var str2 = SecurityCommon.AesEncrypt(str, Key);
            Assert.True(str1 == str2);

            var str3 = str1.AesDecrypt(Key);
            var str4 = SecurityCommon.AesDecrypt(str2,Key);
            Assert.True(str3 == str4);
        }

        /// <summary>
        ///
        /// </summary>
        [Theory]
        [InlineData("123")]
        public void Des(string str)
        {
            var str1 = str.DesEncrypt(Key.Substring(0,8),Key.Substring(0,8));
            var str2 = str1.DesDecrypt(Key.Substring(0,8),Key.Substring(0,8));
            Assert.True(str1==str2);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">需要加密的</param>
        /// <returns></returns>
        public  string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "加密处理失败，加密字符串为空";
            }
            //加密秘钥补位处理
            string encryptKeyall = Convert.ToString(Key);    //定义密钥
            if (encryptKeyall.Length < 9)
            {
                for (; ; )
                {
                    if (encryptKeyall.Length < 9)
                        encryptKeyall += encryptKeyall;
                    else
                        break;
                }
            }
            string encryptKey = encryptKeyall.Substring(0, 8);
            Key = encryptKey;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(str);
            des.Key = ASCIIEncoding.UTF8.GetBytes(Key); // 密匙
            des.IV =ASCIIEncoding.UTF8.GetBytes(Key);  // 向量
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var result = Convert.ToBase64String(ms.ToArray());
            return result;
        }
    }
}
