// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Tools.Component;
using EInfrastructure.Core.Tools.Internal;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 扩展信息
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        ///
        /// </summary>
        static Extensions()
        {
            InitConstellationData();
            InitChinaDate();
            InitWeek();
            InitDateTimeProvider();
        }
    }
}
