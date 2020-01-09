// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.HelpCommon.Randoms.Interface
{
    /// <summary>
    /// 随机数字生成器
    /// </summary>
    public interface IRandomNumberGenerator : Tools.Randoms.Interface.IRandomNumberGenerator, ISingleInstance
    {
    }
}
