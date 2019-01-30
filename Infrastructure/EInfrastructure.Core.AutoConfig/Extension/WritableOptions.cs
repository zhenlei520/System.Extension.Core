using System;
using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EInfrastructure.Core.AutoConfig.Extension
{
    /// <summary>
    /// 设置AppSetting的实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
    {
        private readonly IOptionsMonitor<T> _options;
        private readonly string _section;
        private readonly string _file;

        public WritableOptions(
            IOptionsMonitor<T> options,
            string section,
            string file)
        {
            _options = options;
            _section = section;
            _file = file;
        }

        public T Value => _options.CurrentValue;
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
    }
}