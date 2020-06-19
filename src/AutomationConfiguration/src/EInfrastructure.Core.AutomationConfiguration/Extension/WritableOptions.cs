using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.AutomationConfiguration.Config;
using EInfrastructure.Core.AutomationConfiguration.Interface;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Systems;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EInfrastructure.Core.AutomationConfiguration.Extension
{
    /// <summary>
    /// 设置读写接口的实现类（支持本地文件，如果是需要连接数据库，则需要自行实现IWritableOptions类接口，并且，权重应该大于当前类权重1，接口注入时通过InjectionSelectionCommon.GetImplement<IWritableOptions<T>>(provider)）获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
    {
        private readonly IOptionsMonitor<T> _options;
        private readonly string _section;
        private readonly string _file;
        private readonly string _baseDirectory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        public WritableOptions(IOptionsMonitor<T> options)
        {
            _options = options;
            _section = typeof(T).Name;
            _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="appSettingConfig">配置文件</param>
        public WritableOptions(IOptionsMonitor<T> options, AppSettingConfig appSettingConfig) : this(options)
        {
            var config = GetAppSettingConfig(appSettingConfig);
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
            var physicalPath = Path.Combine(_baseDirectory, _file);
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
            var physicalPath = Path.Combine(_baseDirectory, _file);
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
            var physicalPath = Path.Combine(_baseDirectory, _file);

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
            return 1;
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
        private AppSettingConfig GetAppSettingConfig(AppSettingConfig appSettingConfig)
        {
            var settingConfig = appSettingConfig ?? new AppSettingConfig();
            settingConfig.Maps = SafeList(settingConfig.Maps);
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

        #region 返回安全的集合

        /// <summary>
        /// 返回安全的集合
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> SafeList<T>(ICollection<T> param)
        {
            return ObjectCommon.SafeObject(param != null,
                () => ValueTuple.Create(param?.ToList(), new List<T>()));
        }

        #endregion
    }
}
