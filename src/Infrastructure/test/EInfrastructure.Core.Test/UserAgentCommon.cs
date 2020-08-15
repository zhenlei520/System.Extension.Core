// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.UserAgentParse;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class UserAgentCommon : BaseUnitTest
    {
        public UserAgentCommon() : base()
        {
        }


        [Theory]
        // [InlineData("UCWEB/2.0 (Linux; U; Android 4.4.2; zh-CN; HM NOTE 1LTETD) U2/1.0.0 UCBrowser/9.9.3.478 U2/1.0.0 Mobile")]
        // [InlineData(
        // "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.37")]
        // [InlineData("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36")]
        // [InlineData("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36")]
        // [InlineData("Mozilla/5.0 (X11; Linux x86_64;CentOS; rv:68.0) Gecko/20100101 Firefox/68.0")]
        // [InlineData("CoolPad8720_CMCC_TD/1.0 Linux/3.0.8 Android/4.0 Release/03.31.2013 Browser/AppleWebkit534.3")]
        public void GetUserAgent(string userAgent)
        {
            start:
            try
            {
                var ua = new UserAgent(userAgent).Execute();
                Console.WriteLine(new NewtonsoftJsonProvider().Serializer(ua));
            }
            catch (Exception ex)
            {
                goto start;
            }
        }
    }
}
