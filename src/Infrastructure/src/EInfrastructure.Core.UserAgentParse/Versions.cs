// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Tools;
using Newtonsoft.Json;

namespace EInfrastructure.Core.UserAgentParse
{
    /// <summary>
    /// 版本信息
    /// </summary>
    public class Versions
    {
        public Versions()
        {
        }

        /// <summary>
        /// 完整版本信息
        /// </summary>
        /// <param name="version"></param>
        public Versions(string version) : this()
        {
            var versionList = version.ConvertStrToList<string>('.');
            this.Major = (versionList.Count > 0 ? versionList[0] : "").ConvertToInt();
            this.Second = (versionList.Count > 1 ? versionList[1] : "").ConvertToInt();
            this.Third = (versionList.Count > 2 ? versionList[2] : "").ConvertToInt();
            this.Minor = (versionList.Count > 3 ? versionList[3] : "").ConvertToInt();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="major">主版本号</param>
        /// <param name="second">次版本号</param>
        /// <param name="third">第三版本号</param>
        /// <param name="minor">最小版本号</param>
        public Versions(string major, string second, string third, string minor) : this()
        {
            Major = major.ConvertToInt();
            Second = second.ConvertToInt();
            Third = third.ConvertToInt();
            Minor = minor.ConvertToInt();
        }

        /// <summary>
        /// 主版本号
        /// </summary>
        [JsonProperty(PropertyName = "major",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Major { get; set; }

        /// <summary>
        /// 次版本号
        /// </summary>
        [JsonProperty(PropertyName = "second",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Second { get; set; }

        /// <summary>
        /// 第三版本号
        /// </summary>
        [JsonProperty(PropertyName = "third",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Third { get; set; }

        /// <summary>
        /// 最小版本号
        /// </summary>
        [JsonProperty(PropertyName = "minor",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Minor { get; set; }

        #region 重写运算符

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static bool operator >(Versions source, Versions opt)
        {
            return source.Major > opt.Major || (source.Major == opt.Major && source.Second > opt.Second) ||
                   (source.Major == opt.Major && source.Second == opt.Second && source.Third > opt.Third) ||
                   (source.Major == opt.Major && source.Second == opt.Second && source.Third == opt.Third &&
                    source.Minor > opt.Minor);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static bool operator >=(Versions source, Versions opt)
        {
            return source.Major > opt.Major || (source.Major == opt.Major && source.Second > opt.Second) ||
                   (source.Major == opt.Major && source.Second == opt.Second && source.Third >= opt.Third) ||
                   (source.Major == opt.Major && source.Second == opt.Second && source.Third == opt.Third &&
                    source.Minor == opt.Minor);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static bool operator <(Versions source, Versions opt)
        {
            return source.Major < opt.Major || (source.Major == opt.Major && source.Second < opt.Second) ||
                   (source.Major == opt.Major && source.Second == opt.Second && source.Third < opt.Third) ||
                   (source.Major == opt.Major && source.Second == opt.Second && source.Third == opt.Third &&
                    source.Minor < opt.Minor);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static bool operator <=(Versions source, Versions opt)
        {
            return source.Major < opt.Major || (source.Major == opt.Major && source.Second < opt.Second) ||
                   (source.Major == opt.Major && source.Second == opt.Second && source.Third <= opt.Third) ||
                   (source.Major == opt.Major && source.Second == opt.Second && source.Third == opt.Third &&
                    source.Minor <= opt.Minor);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool operator >(Versions source, string version)
        {
            if (source == null)
            {
                return false;
            }

            var opt = new Versions(version);
            return source > opt;
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool operator >=(Versions source, string version)
        {
            if (source == null)
            {
                return false;
            }

            var opt = new Versions(version);
            return source >= opt;
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool operator <(Versions source, string version)
        {
            if (source == null)
            {
                return true;
            }

            var opt = new Versions(version);
            return source < opt;
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool operator <=(Versions source, string version)
        {
            if (source == null)
            {
                return true;
            }

            var opt = new Versions(version);
            return source <= opt;
        }

        #endregion

        /// <summary>
        /// 输出版本号
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var version = "";
            if (Major != null)
            {
                version += Major;
            }

            if (Second != null || Third != null || Minor != null)
            {
                version += "." + (Second ?? 0);
            }

            if (Third != null || Minor != null)
            {
                version += "." + (Third ?? 0);
            }

            if (Minor != null)
            {
                version += "." + Minor;
            }

            return version;
        }
    }
}
