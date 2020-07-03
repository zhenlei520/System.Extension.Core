// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs
{
    /// <summary>
    /// Xml 序列化帮助类
    /// </summary>
    public interface IXmlProvider : IIdentify, ISingleInstance
    {
        /// <summary>
        /// 序列化为xml字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ifNameSpace">是否强制指定命名空间，覆盖默认的命名空间</param>
        /// <param name="nameSpaceDic">默认为null（移除默认命名空间）</param>
        /// <param name="encodingFormat">编码格式(默认utf8)</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string Serializer<T>(T obj, bool ifNameSpace = true,
            Dictionary<string, string> nameSpaceDic = null, Encoding encodingFormat = null);

        /// <summary>
        /// xml字符串反序列化
        /// </summary>
        /// <param name="xml">待反序列化的字符串</param>
        /// <param name="encoding">编码格式，默认utf8</param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Deserialize<T>(string xml, Encoding encoding = null, Func<System.Exception, T> func = null);

        /// <summary>
        /// 获取Xml根节点名称
        /// </summary>
        /// <param name="xmlStr">xml字符串</param>
        /// <returns></returns>
        XmlElement GetXmlElement(string xmlStr);
    }
}
