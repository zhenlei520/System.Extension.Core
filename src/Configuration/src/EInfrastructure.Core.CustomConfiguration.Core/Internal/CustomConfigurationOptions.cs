// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Systems;
using EInfrastructure.Core.Validation;
using FluentValidation;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    ///
    /// </summary>
    public class CustomConfigurationOptions : IFluentlValidatorEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomConfigurationOptions()
        {
            Duration = 30 * 1000;
        }

        #region 得到缓存文件路径

        /// <summary>
        /// 缓存文件路径
        /// </summary>
        internal string GetCacheFileDirectoryPath => CacheFileDirectoryPath.GetNotNullAndNotEmpty(
            Path.Combine(EnvironmentCommon.GetOs.IsLinux ? "/opt/data" : @"C:\opt\data", Appid));

        #endregion

        /// <summary>
        /// 多久读取一次配置去更新（单位：ms）
        /// 默认30s执行一次
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 名称空间
        /// </summary>
        public List<string> Namespaces { get; set; }

        /// <summary>
        /// 环境名称
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// 缓存文件夹地址
        /// </summary>
        public string CacheFileDirectoryPath { get; set; }

        /// <summary>
        ///
        /// </summary>
        internal ICustomConfigurationOptionsExtension Extensions { get; set; }

        #region methods

        #region Registers an extension that will be executed when building services.

        /// <summary>
        /// Registers an extension that will be executed when building services.
        /// </summary>
        /// <param name="extension"></param>
        public void RegisterExtension(ICustomConfigurationOptionsExtension extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            if (Extensions == null)
            {
                Extensions = extension;
            }
        }

        /// <summary>
        /// 得到缓存文件
        /// </summary>
        /// <param name="namespaces">名称空间</param>
        /// <returns></returns>
        internal string GetCacheFile(string namespaces)
        {
            return Path.Combine(GetCacheFileDirectoryPath, namespaces);
        }
        #endregion

        #region 添加namespaces

        /// <summary>
        /// 添加namespaces
        /// </summary>
        /// <param name="namespaces">名称空间</param>
        public void AddNamespace(params string[] namespaces)
        {
            Namespaces = Namespaces ?? new List<string>();
            Namespaces.AddRange(namespaces);
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomConfigurationOptionsValidator : AbstractValidator<CustomConfigurationOptions>,
        IFluentlValidator<CustomConfigurationOptions>
    {
        public CustomConfigurationOptionsValidator()
        {
            RuleFor(x => x.Appid).Must(x => !string.IsNullOrEmpty(x)).WithMessage("Appid is not empty");
            RuleFor(x => x.EnvironmentName).Must(x => !string.IsNullOrEmpty(x))
                .WithMessage("EnvironmentName is not empty");
            RuleFor(x => x.Namespaces).Must(x => x != null && x.Count > 0).WithMessage("Namespaces is not empty");
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(0)
                .WithMessage("Duration has to be greater than or equal to 0");
            RuleFor(x => x.Extensions).NotNull().WithMessage("Extensions is not empty");
        }
    }
}