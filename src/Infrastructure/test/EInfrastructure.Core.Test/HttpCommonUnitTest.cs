// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Test.Base;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class HttpCommonUnitTest : BaseUnitTest
    {
        public HttpCommonUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public string GetResponse()
        {
            HttpClient client = new HttpClient("");
            var res = client.GetStringByPost("", new{});
            return res;
        }


        [Fact]
        public string GetResponse2()
        {
            HttpClient client = new HttpClient("")
            {
                Headers = new Dictionary<string, string>()
                {
                    {"Cookie",""}
                }
            };
            var res = client.GetString($"");
            return res;
        }
    }
}
