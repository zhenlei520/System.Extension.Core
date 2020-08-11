// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Tools.UserAgentParse
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
            this.Major = (versionList.Count > 0 ? versionList[0] : "").ConvertToDecimal();
            this.Second = (versionList.Count > 1 ? versionList[1] : "").ConvertToDecimal();
            this.Minor = (versionList.Count > 2 ? versionList[2] : "").ConvertToDecimal();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="major">主版本号</param>
        /// <param name="second">次版本号</param>
        /// <param name="minor">最小版本号</param>
        public Versions(string major, string second, string minor) : this()
        {
            Major = major.ConvertToDecimal();
            Second = second.ConvertToDecimal();
            Minor = minor.ConvertToDecimal();
        }

        /// <summary>
        /// 主版本号
        /// </summary>
        public decimal? Major { get; set; }

        /// <summary>
        /// 次版本号
        /// </summary>
        public decimal? Second { get; set; }

        /// <summary>
        /// 最小版本号
        /// </summary>
        public decimal? Minor { get; set; }

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
                   (source.Major == opt.Major && source.Second == opt.Second && source.Minor > opt.Minor);
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
                   (source.Major == opt.Major && source.Second == opt.Second && source.Minor >= opt.Minor);
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
                   (source.Major == opt.Major && source.Second == opt.Second && source.Minor < opt.Minor);
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
                   (source.Major == opt.Major && source.Second == opt.Second && source.Minor <= opt.Minor);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="source"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool operator >(Versions source, string version)
        {
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
            var opt = new Versions(version);
            return source <= opt;
        }

        #endregion

        public override string ToString()
        {
            var version = "";
            if (Major != null)
            {
                version += Major;
            }

            if (Second != null || Minor != null)
            {
                version += ("." + (Second ?? 0));
            }

            if (Minor != null)
            {
                version += "," + version;
            }

            return version;
        }
    }
}
