using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.AutomationConfiguration.Config;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Systems;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EInfrastructure.Core.AutomationConfiguration.Extension
{
    /// <summary>
    /// 设置读写接口的实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WritableOptions<T> : IIdentify, IDependency, IWritableOptions<T> where T : class, new()
    {
        private readonly IOptionsMonitor<T> _options;
        private readonly string _section;
        private readonly string _file;

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="appSettingConfig">配置文件</param>
        public WritableOptions(IOptionsMonitor<T> options, IOptions<AppSettingConfig> appSettingConfig)
        {
            _options = options;
            var config = GetAppSettingConfig(appSettingConfig);
            _section = nameof(T);
            _file = GetFilePath(config);
        }

        /// <summary>
        ///
        /// </summary>
        public T Value => _options.CurrentValue;

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Get(string name) => _options.Get(name);

        #region 得到配置信息

        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            return Value;
        }

        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public TSource Get<TSource>() where TSource : class, new()
        {
            var physicalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _file);
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
            var sectionObject = jObject.TryGetValue(_section, out JToken section)
                ? JsonConvert.DeserializeObject<TSource>(section.ToString())
                : (JsonConvert.DeserializeObject<TSource>(JsonConvert.SerializeObject(Value, Formatting.Indented)) ??
                   new TSource());
            return sectionObject;
        }

        #endregion

        #region 更新配置

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="applyChanges"></param>
        public void Update(Action<T> applyChanges)
        {
            var physicalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _file);

            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
            var sectionObject = jObject.TryGetValue(_section, out JToken section)
                ? JsonConvert.DeserializeObject<T>(section.ToString())
                : (Value ?? new T());

            applyChanges(sectionObject);

            jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
            File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="applyChanges"></param>
        public void Update<TSource>(Action<TSource> applyChanges)
            where TSource : class, new()
        {
            var physicalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _file);

            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
            var sectionObject = jObject.TryGetValue(_section, out JToken section)
                ? JsonConvert.DeserializeObject<TSource>(section.ToString())
                : (JsonConvert.DeserializeObject<TSource>(JsonConvert.SerializeObject(Value, Formatting.Indented)) ??
                   new TSource());

            applyChanges(sectionObject);

            jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
            File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }

        #endregion

        #region 权重

        /// <summary>
        /// 权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 99;
        }

        #endregion

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region 得到文件地址

        #region 得到配置

        /// <summary>
        /// 得到配置
        /// </summary>
        /// <param name="appSettingConfig"></param>
        /// <returns></returns>
        private AppSettingConfig GetAppSettingConfig(IOptions<AppSettingConfig> appSettingConfig)
        {
            var settingConfig = appSettingConfig?.Value ?? new AppSettingConfig();
            settingConfig.Maps = settingConfig.Maps.SafeList();
            settingConfig.DefaultPath = settingConfig.DefaultPath.SafeString();
            return settingConfig;
        }

        #endregion

        #region 得到文件地址

        /// <summary>
        /// 得到文件地址
        /// </summary>
        /// <returns></returns>
        private string GetFilePath(AppSettingConfig appSettingConfig)
        {
            var path = appSettingConfig.Maps.Where(x => x.Key == _section).Select(x => x.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(path))
            {
                return appSettingConfig.DefaultPath;
            }

            return path;
        }

        #endregion

        #endregion
    }
}
