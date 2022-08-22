// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Tests.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Tests
{
    public class EMailCommonUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData("wangzhenlei520@gmail.com", "github挺好使的", "不错，挺好用的")]
        public void SendEmail(string toEmail, string title, string body)
        {
            EMailCommon.SendEmail(toEmail, title, body, "", "", "", "成功");
        }
    }
}
