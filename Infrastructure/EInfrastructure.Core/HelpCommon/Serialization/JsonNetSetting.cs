using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EInfrastructure.Core.HelpCommon.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class NullToEmptyStringResolver : DefaultContractResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                .Select(p =>
                {
                    var jp = base.CreateProperty(p, memberSerialization);
                    jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                    return jp;
                }).ToList();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NullToEmptyStringValueProvider : IValueProvider
    {
        readonly PropertyInfo _memberInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _memberInfo = memberInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            object result = _memberInfo.GetValue(target);
            if (_memberInfo.PropertyType == typeof(string) && result == null)
            {
                result = "";
            }
            else if ((_memberInfo.PropertyType == typeof(DateTime) || _memberInfo.PropertyType == typeof(DateTime?)) &&
                     result != null)
            {
                if (result.ToString() == "0001/1/1 0:00:00" || result.ToString() == "1000/1/1 0:00:00")
                {
                    result = "";
                }

                if (!string.IsNullOrEmpty(result.ToString()))
                {
                    DateTime? time = result.ConvertToDateTime(null);

                    if (time == null || time == DateTime.MaxValue ||
                        time == DateTime.MinValue)
                    {
                        result = "";
                    }
                }
            }
            else if (_memberInfo.PropertyType.Name == "List`1" && result == null)
            {
                result = new List<object>();
            }
            else if (_memberInfo.PropertyType.Name == "Object" && result == null)
            {
                result = new { };
            }

            return result;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            _memberInfo.SetValue(target, value);
        }
    }
}