// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Params;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor
{
    /// <summary>
    /// 图片审查服务
    /// </summary>
    public interface IPictureCensorProvider : IIdentify, ISingleInstance
    {
        /// <summary>
        /// 图片检测
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseCensorResponseDto Censor(PictureCensorParam param);
    }
}
