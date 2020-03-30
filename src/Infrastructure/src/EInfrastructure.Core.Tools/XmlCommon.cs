// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Xml;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// xml帮助类
    /// </summary>
    public class XmlCommon
    {
        #region 获取Xml根节点名称

        /// <summary>
        /// 获取Xml根节点名称
        /// </summary>
        /// <param name="xmlStr">xml字符串</param>
        /// <returns></returns>
        public static XmlElement GetXmlElement(string xmlStr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStr);
            return xmlDoc.DocumentElement;
        }

        #endregion
    }
}
