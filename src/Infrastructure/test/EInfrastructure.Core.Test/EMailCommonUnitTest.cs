// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Test.Base;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    public class EMailCommonUnitTest : BaseUnitTest
    {
        public EMailCommonUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("wangzhenlei520@gmail.com", "github挺好使的", "不错，挺好用的")]
        public void SendEmail(string toEmail, string title, string body)
        {
            EMailCommon.SendEmail(toEmail, title, body, "", "", "", "成功");
        }
    }
}
