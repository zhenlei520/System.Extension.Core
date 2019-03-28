// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Net;

namespace EInfrastructure.Core.UCloud.Storage.Common
{
    internal static class HttpWebResponseExt
    {
        internal static HttpWebResponse GetResponseNoException(this HttpWebRequest req) {
            try { 
                return (HttpWebResponse)req.GetResponse();
            }catch (WebException we) {
                var resp = we.Response as HttpWebResponse;
                if (null == resp) throw;
                return resp;
            }
        }
    }
}