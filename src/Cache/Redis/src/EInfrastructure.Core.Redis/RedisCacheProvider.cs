using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Redis.Common;
using EInfrastructure.Core.Redis.Config;
using EInfrastructure.Core.Redis.Validator;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Enumerations;
using EInfrastructure.Core.Validation.Common;

namespace EInfrastructure.Core.Redis
{
    /// <summary>
    /// Redis缓存服务
    /// </summary>
    public class RedisCacheProvider : BaseRedisCacheProvider, ICacheProvider
    {
        /// <summary>
        /// 前缀
        /// </summary>
        private readonly string _prefix;

        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        public RedisCacheProvider(RedisConfig redisConfig, ICollection<IJsonProvider> jsonProviders)
        {
            _jsonProvider = InjectionSelectionCommon.GetImplement(jsonProviders);
            ValidationCommon.Check(redisConfig, "redis配置异常", HttpStatus.Err.Name);
            new RedisConfigValidator().Validate(redisConfig).Check();
            _prefix = redisConfig.Prefix;
            CsRedisHelper.InitializeConfiguration(redisConfig);
        }

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

        #region Methods

        #region String

        #region 同步方法

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="overdueStrategy"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?),
            OverdueStrategy overdueStrategy = null)
        {
            return base.Execute(overdueStrategy, () => QuickHelperBase.Set(key, value,
                expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1), () => { return false; });
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <param name="overdueStrategy"></param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?),
            OverdueStrategy overdueStrategy = null)
        {
            return base.Execute(overdueStrategy, () => QuickHelperBase.Set(key, ConvertJson(obj),
                expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1), () => { return false; });
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            return QuickHelperBase.Get(key);
        }

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKeys">Redis Key集合</param>
        /// <returns></returns>
        public List<string> StringGet(List<string> listKeys)
        {
            return Enumerable.ToList<string>(QuickHelperBase.GetStrings(listKeys.ToArray()));
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key) where T : class, new()
        {
            return ConvertObj<T>(StringGet(key));
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public long StringIncrement(string key, long val = 1)
        {
            return QuickHelperBase.Increment(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public long StringDecrement(string key, long val = 1)
        {
            return QuickHelperBase.Increment(key, 0 - val);
        }

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="overdueStrategy"></param>
        /// <returns></returns>
        public Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?),
            OverdueStrategy overdueStrategy = null)
        {
            return base.Execute(overdueStrategy, () =>
                {
                    return QuickHelperBase.SetAsync(key, value,
                        expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1);
                },
                () => { return Task.FromResult(false); });
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <param name="overdueStrategy"></param>
        /// <returns></returns>
        public Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?),
            OverdueStrategy overdueStrategy = null)
        {
            return base.Execute(overdueStrategy, () => QuickHelperBase.SetAsync(key, ConvertJson<T>(obj),
                    expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1),
                () => { return Task.FromResult(false); });
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public Task<string> StringGetAsync(string key)
        {
            return QuickHelperBase.GetAsync(key);
        }


        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public Task<long> StringIncrementAsync(string key, long val = 1)
        {
            return QuickHelperBase.IncrementAsync(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public Task<long> StringDecrementAsync(string key, long val = 1)
        {
            return QuickHelperBase.IncrementAsync(key, 0 - val);
        }

        #endregion 异步方法

        #endregion String

        #region Hash

        #region 同步方法

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            return QuickHelperBase.HashExists(key, dataKey);
        }

        #region 存储数据到hash表

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <param name="second">秒</param>
        /// <param name="isSetHashKeyExpire">false：设置key的过期时间（整个键使用一个过期时间），true：设置hashkey的过期时间，默认设置的为HashKey的过期时间（单个datakey使用一个过期时间）。</param>
        /// <param name="overdueStrategy"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string dataKey, T t, long second = -1L, bool isSetHashKeyExpire = true,
            OverdueStrategy overdueStrategy = null)
        {
            string value = "";
            return base.Execute(overdueStrategy, () =>
            {
                if (!isSetHashKeyExpire)
                {
                    value =
                        QuickHelperBase.HashSetExpire(key, GetExpire(second), dataKey, ConvertJson(t));
                }
                else
                {
                    value = QuickHelperBase.HashSetHashFileExpire(GetKey(key), GetKey(dataKey), GetExpire(second),
                        ConvertJson(t));
                }

                bool result = string.Equals(value, "OK",
                    StringComparison.OrdinalIgnoreCase);
                return result;
            }, () => { return false; });
        }

        /// <summary>
        ///  存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="kvalues"></param>
        /// <param name="second">秒</param>
        /// <param name="isSetHashKeyExpire"></param>
        /// <param name="overdueStrategy"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, Dictionary<string, T> kvalues, long second = -1L,
            bool isSetHashKeyExpire = true, OverdueStrategy overdueStrategy = null)
        {
            return base.Execute(overdueStrategy, () =>
            {
                List<object> keyValues = new List<object>();
                foreach (var kvp in kvalues)
                {
                    keyValues.Add(isSetHashKeyExpire ? GetKey(kvp.Key) : kvp.Key);
                    keyValues.Add(ConvertJson(kvp.Value));
                }

                if (isSetHashKeyExpire)
                {
                    return string.Equals(
                        QuickHelperBase.HashSetHashFileExpire(GetKey(key), GetExpire(second), keyValues.ToArray()),
                        "OK",
                        StringComparison.OrdinalIgnoreCase);
                }

                return string.Equals(QuickHelperBase.HashSetExpire(key, GetExpire(second), keyValues.ToArray()), "OK",
                    StringComparison.OrdinalIgnoreCase);
            }, () => { return false; });
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <param name="kValues"></param>
        /// <param name="second"></param>
        /// <param name="isSetHashKeyExpire">是否制定HashKey的过期时间</param>
        /// <param name="overdueStrategy"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HashSet<T>(Dictionary<string, Dictionary<string, T>> kValues, long second = -1L,
            bool isSetHashKeyExpire = true, OverdueStrategy overdueStrategy = null)
        {
            return base.Execute(overdueStrategy, () =>
            {
                Dictionary<string, object[]> keyValues = new Dictionary<string, object[]>();
                foreach (var item in kValues)
                {
                    List<object> dataKeyValues = new List<object>();
                    foreach (var kvp in item.Value)
                    {
                        dataKeyValues.Add(isSetHashKeyExpire ? GetKey(kvp.Key) : kvp.Key);
                        dataKeyValues.Add(ConvertJson(kvp.Value));
                    }

                    keyValues.Add(isSetHashKeyExpire ? GetKey(item.Key) : item.Key, dataKeyValues.ToArray());
                }

                if (isSetHashKeyExpire)
                {
                    return string.Equals(QuickHelperBase.HashSetHashFileExpire(keyValues, GetExpire(second)), "OK",
                        StringComparison.OrdinalIgnoreCase);
                }

                return string.Equals(QuickHelperBase.HashSetExpire(keyValues, GetExpire(second)), "OK",
                    StringComparison.OrdinalIgnoreCase);
            }, () => { return false; });
        }

        #endregion

        #region 清除过期的hashkey(自定义hashkey删除)

        /// <summary>
        /// 清除过期的hashkey(自定义hashkey删除)
        /// </summary>
        /// <param name="count">指定清除指定数量的已过期的hashkey</param>
        /// <returns></returns>
        public bool ClearOverTimeHashKey(long count = 1000L)
        {
            var list = SortedSetRangeByRankAndOverTime(count);
            if (list.Count != 0)
            {
                list.ForEach(item =>
                {
                    HashDelete(item.Item3, item.Item4);
                    SortedSetRemove(item.Item1, item.Item2);
                });
            }

            return true;
        }

        #endregion

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string dataKey)
        {
            return QuickHelperBase.HashDelete(key, dataKey) >= 0;
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long HashDelete(string key, List<string> dataKeys)
        {
            return QuickHelperBase.HashDelete(key, dataKeys.ToArray());
        }

        #region 从hash表获取数据

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string dataKey) where T : class, new()
        {
            var str = HashGet(key, dataKey);
            return ConvertObj<T>(str);
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public string HashGet(string key, string dataKey)
        {
            return QuickHelperBase.HashGet(key, dataKey);
        }

        #endregion

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public Dictionary<string, string> HashGet(string key, List<string> dataKeys)
        {
            if (dataKeys != null && dataKeys.Count > 0)
            {
                dataKeys = dataKeys.Distinct().ToList();
                var values = QuickHelperBase.HashGet(key, dataKeys.ToArray()).ToList();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                for (int i = 0; i < dataKeys.Count; i++)
                {
                    if (!dic.ContainsKey(dataKeys[i]) && values[i] != null)
                    {
                        dic.Add(dataKeys[i], values[i]);
                    }
                }

                return dic;
            }

            return QuickHelperBase.HashGetAll(key);
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, string>> HashGet(Dictionary<string, string[]> keys)
        {
            Dictionary<string, Dictionary<string, string>> dic =
                new Dictionary<string, Dictionary<string, string>>();
            if (keys != null && keys.Count > 0)
            {
                Dictionary<string, string[]> values = QuickHelperBase.HashGet(keys);

                foreach (var item in keys)
                {
                    string[] valuesList = values.Where(x => x.Key == string.Concat(QuickHelperBase.Name, item.Key))
                        .Select(x => x.Value).FirstOrDefault();
                    Dictionary<string, string> newDic = new Dictionary<string, string>();
                    if (valuesList != null && valuesList.Length > 0)
                    {
                        for (int i = 0; i < item.Value.Length; i++)
                        {
                            if (!newDic.ContainsKey(item.Value[i]) && valuesList[i] != null)
                            {
                                newDic.Add(item.Value[i], valuesList[i]);
                            }
                        }
                    }

                    dic.Add(item.Key, newDic);
                }
            }

            return dic;
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public long HashIncrement(string key, string dataKey, long val = 1)
        {
            return QuickHelperBase.HashIncrement(key, dataKey, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public long HashDecrement(string key, string dataKey, long val = 1)
        {
            return QuickHelperBase.HashIncrement(key, dataKey, 0 - val);
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> HashKeys(string key)
        {
            return Enumerable.ToList<string>(QuickHelperBase.HashKeys(key));
        }

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Task<bool> HashExistsAsync(string key, string dataKey)
        {
            return QuickHelperBase.HashExistsAsync(key, dataKey);
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <param name="overdueStrategy"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string key, string dataKey, T t, OverdueStrategy overdueStrategy = null)
        {
            return (await base.Execute(overdueStrategy,
                () => { return QuickHelperBase.HashSetAsync(key, dataKey, ConvertJson<T>(t)); },
                () => { return Task.FromResult("False"); })).Equals("TRUE", StringComparison.OrdinalIgnoreCase);
            // return string.Equals(await QuickHelperBase.HashSetAsync(key, dataKey, ConvertJson<T>(t)), "TRUE",
            //     StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string dataKey)
        {
            return await QuickHelperBase.HashDeleteAsync(key, dataKey) >= 0;
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public Task<long> HashDeleteAsync(string key, List<string> dataKeys)
        {
            return QuickHelperBase.HashDeleteAsync(key, dataKeys.ToArray());
        }

        #region 从hash表获取数据

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string dataKey) where T : class, new()
        {
            return ConvertObj<T>(await QuickHelperBase.HashGetAsync(key, dataKey));
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Task<string> HashGetAsync(string key, string dataKey)
        {
            return QuickHelperBase.HashGetAsync(key, dataKey);
        }

        #endregion

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public Task<long> HashIncrementAsync(string key, string dataKey, long val = 1)
        {
            return QuickHelperBase.HashIncrementAsync(key, dataKey, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public Task<long> HashDecrementAsync(string key, string dataKey, long val = 1)
        {
            return QuickHelperBase.HashIncrementAsync(key, dataKey, 0 - val);
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<string>> HashKeysAsync(string key)
        {
            return Enumerable.ToList<string>(await QuickHelperBase.HashKeysAsync(key));
        }

        #endregion 异步方法

        #endregion Hash

        #region List

        #region 同步方法

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRemove<T>(string key, T value)
        {
            return QuickHelperBase.LRem(key, int.MaxValue, ConvertJson(value));
        }

        #region 获取指定key的List

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<string> ListRange(string key, long count = 1000)
        {
            return QuickHelperBase.LRang(key, 0, count).ToList();
        }

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> ListRange<T>(string key, long count = 1000) where T : class, new()
        {
            List<T> list = new List<T>();
            QuickHelperBase.LRang(key, 0, count).ToList().ForEach(p => { list.Add(ConvertObj<T>(p)); });
            return list;
        }

        #endregion

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string key, T value)
        {
            return QuickHelperBase.RPush(key, new string[1] {ConvertJson<T>(value)});
        }

        #region 出队

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListRightPop(string key)
        {
            return QuickHelperBase.RPop(key);
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key) where T : class, new()
        {
            return ConvertObj<T>(QuickHelperBase.RPop(key));
        }

        #endregion

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string key, T value)
        {
            return QuickHelperBase.LPush(key, new string[1] {ConvertJson<T>(value)});
        }

        #region 出栈

        /// <summary>
        /// 出栈
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListLeftPop(string key)
        {
            return QuickHelperBase.LPop(key);
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key) where T : class, new()
        {
            return ConvertObj<T>(QuickHelperBase.LPop(key));
        }

        #endregion

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return QuickHelperBase.LLen(key);
        }

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> ListRemoveAsync<T>(string key, T value)
        {
            return QuickHelperBase.LRemAsync(key, int.MaxValue, ConvertJson(value));
        }

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<string>> ListRangeAsync(string key, long count = 1000)
        {
            return (await QuickHelperBase.LRangAsync(key, 0, count)).ToList();
        }

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<T>> ListRangeAsync<T>(string key, long count = 1000) where T : class, new()
        {
            return ConvertListObj<T>((await QuickHelperBase.LRangAsync(key, 0, count)).ToList());
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> ListRightPushAsync<T>(string key, T value)
        {
            return QuickHelperBase.RPushAsync(key, new[] {ConvertJson(value)});
        }

        #region 出队

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<string> ListRightPopAsync(string key)
        {
            return QuickHelperBase.RPopAsync(key);
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string key) where T : class, new()
        {
            return ConvertObj<T>(await QuickHelperBase.RPopAsync(key));
        }

        #endregion

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> ListLeftPushAsync<T>(string key, T value)
        {
            return QuickHelperBase.LPushAsync(key, new string[1] {ConvertJson<T>(value)});
        }

        #region 出栈

        /// <summary>
        /// 出栈
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<string> ListLeftPopAsync(string key)
        {
            return QuickHelperBase.LPopAsync(key);
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key) where T : class, new()
        {
            return ConvertObj<T>(await QuickHelperBase.LPopAsync(key));
        }

        #endregion

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<long> ListLengthAsync(string key)
        {
            return QuickHelperBase.LLenAsync(key);
        }

        #endregion 异步方法

        #endregion List

        #region SortedSet

        #region 同步方法

        /// <summary>
        /// 添加 (当score一样value一样时不插入)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <param name="isOverlap"></param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string key, T value, double score, bool isOverlap = false)
        {
            if (isOverlap)
            {
                SortedSetRemove<T>(key, value);
            }

            return QuickHelperBase.ZAdd(key, (score, ConvertJson<T>(value))) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SortedSetRemove<T>(string key, T value)
        {
            return QuickHelperBase.ZRem(key, new string[1] {ConvertJson<T>(value)}) > 0;
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<string> SortedSetRangeByRank(string key, long count = 1000)
        {
            return QuickHelperBase.ZRange(key, 0, count).ToList();
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> SortedSetRangeByRank<T>(string key, long count = 1000) where T : class, new()
        {
            return ConvertListObj<T>(QuickHelperBase.ZRange(key, 0, count).ToList());
        }

        #region 获取已过期的hashKey

        /// <summary>
        /// 获取已过期的hashKey
        /// 其中Item1为SortSet的Key，Item2为SortSet的Value,Item3为HashSet的Key，Item4为HashSet的HashKey
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ValueTuple<string, string, string, string>> SortedSetRangeByRankAndOverTime(long count = 1000)
        {
            var keyList = QuickHelperBase
                .ZRevRangeByScore(QuickHelperBase.GetCacheFileKeys(),
                    DateTime.Now.ToUnixTimestamp(TimestampType.Millisecond), 0, count,
                    null); //得到过期的key集合
            List<ValueTuple<string, string, string, string>> result = new List<(string, string, string, string)>();
            keyList.ForEach(item =>
            {
                for (int i = 0; i < item.Item2.Length; i += 2)
                {
                    result.Add((item.Item1.Replace(_prefix, ""), item.Item2[i].ToString(),
                        item.Item2[i].ToString().Replace("~_~", "！").Split('！')[0],
                        item.Item2[i].ToString().Replace("~_~", "！").Split('！')[1]));
                }
            });
            return result;
        }

        #endregion

        #region 降序获取指定索引的集合

        /// <summary>
        /// 降序获取指定索引的集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        public List<string> GetRangeFromSortedSetDesc(string key, long fromRank, long toRank)
        {
            return QuickHelperBase.ZRevRange(key, fromRank, toRank).ToList();
        }

        /// <summary>
        /// 降序获取指定索引的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        public List<T> GetRangeFromSortedSetDesc<T>(string key, long fromRank, long toRank) where T : class, new()
        {
            return ConvertListObj<T>(QuickHelperBase.ZRevRange(key, fromRank, toRank).ToList());
        }

        #endregion

        #region 获取指定索引的集合

        /// <summary>
        /// 获取指定索引的集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        public List<string> GetRangeFromSortedSet(string key, long fromRank, long toRank)
        {
            return QuickHelperBase.ZRange(key, fromRank, toRank).ToList();
        }

        /// <summary>
        /// 获取指定索引的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        public List<T> GetRangeFromSortedSet<T>(string key, long fromRank, long toRank) where T : class, new()
        {
            return ConvertListObj<T>(Enumerable.ToList(QuickHelperBase.ZRange(key, fromRank, toRank)));
        }

        #endregion

        #region 判断是否存在项

        /// <summary>
        /// 判断是否存在项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SortedSetExistItem<T>(string key, T value)
        {
            return QuickHelperBase.ZScore(key, ConvertJson(value)).HasValue;
        }

        #endregion

        #region 获取集合中的数量

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            return QuickHelperBase.ZCard(key);
        }

        #endregion

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string key, T value, double score)
        {
            return await QuickHelperBase.ZAddAsync(key, (score, ConvertJson<T>(value))) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            return await QuickHelperBase.ZRemAsync(key, ConvertJson(value)) > 0;
        }

        #region 获取全部

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<string>> SortedSetRangeByRankAsync(string key, long count = 1000)
        {
            return (await QuickHelperBase.ZRangeAsync(key, 0, count)).ToList();
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<T>> SortedSetRangeByRankAsync<T>(string key, long count = 1000) where T : class, new()
        {
            return ConvertListObj<T>((await QuickHelperBase.ZRangeAsync(key, 0, count)).ToList());
        }

        #endregion

        #region 获取集合中的数量

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<long> SortedSetLengthAsync(string key)
        {
            return QuickHelperBase.ZCardAsync(key);
        }

        #endregion

        #endregion 异步方法

        #endregion SortedSet 有序集合

        #region Basics

        #region 删除指定Key的缓存

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns>返回删除的数量</returns>
        public long Remove(List<string> keys)
        {
            return QuickHelperBase.Remove(keys?.ToArray());
        }

        #endregion

        #region 删除指定Key的缓存

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns>返回删除的数量</returns>
        public long Remove(params string[] keys)
        {
            return QuickHelperBase.Remove(keys);
        }

        #endregion

        #region 检查给定 key 是否存在

        /// <summary>
        /// 检查给定 key 是否存在
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public bool Exist(string key)
        {
            return QuickHelperBase.Exists(key);
        }

        #endregion

        #region 设置指定key过期时间

        /// <summary>
        /// 设置指定key过期时间
        /// </summary>
        /// <param name="key">不含prefix前辍RedisHelper.Name</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan expire)
        {
            return QuickHelperBase.Expire(key, expire);
        }

        #endregion

        #region 查找所有符合给定模式( pattern)的 key

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key
        /// </summary>
        /// <param name="pattern">如：runoob*，不含prefix前辍RedisHelper.Name</param>
        /// <returns></returns>
        public List<string> Keys(string pattern)
        {
            var keys = new List<string>();
            QuickHelperBase.Keys(_prefix + pattern).ToList().ForEach(p => { keys.Add(p.Substring(_prefix.Length)); });
            return keys;
        }

        #endregion

        #endregion Basics

        #endregion

        #region 辅助方法

        #region 将对象序列化成JSON

        /// <summary>
        /// 将对象序列化成JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() : _jsonProvider.Serializer(value);
            return result;
        }

        #endregion

        #region 将JSON反序列化成对象

        /// <summary>
        /// 序列化列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<T> ConvertListObj<T>(List<string> values) where T : class, new()
        {
            List<T> list = new List<T>();
            values.ForEach(p => { list.Add(ConvertObj<T>(p)); });
            return list;
        }

        /// <summary>
        /// 将JSON反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private T ConvertObj<T>(string value) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(T);
            }

            Type t = typeof(T);
            if (string.Equals(t.Name, "string", StringComparison.OrdinalIgnoreCase))
            {
                return (T) Convert.ChangeType(value, typeof(T));
            }

            return _jsonProvider.Deserialize<T>(value);
        }

        #endregion

        #region 将一个object对象序列化，返回一个byte[]

        /// <summary>
        ///  将一个object对象序列化，返回一个byte[]
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns></returns>
        private byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        #endregion

        #region 将一个序列化后的byte[]数组还原

        /// <summary>
        /// 将一个序列化后的byte[]数组还原
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public object BytesToObject(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        #endregion

        #region 获取过期时间

        /// <summary>
        /// 获取过期时间
        /// </summary>
        /// <param name="second"></param>
        private TimeSpan GetExpire(long second = -1)
        {
            TimeSpan timeSpan = default(TimeSpan);
            if (second == 0)
            {
                timeSpan = TimeSpan.Zero;
            }
            else if (second > 0)
            {
                timeSpan = DateTime.Now.AddSeconds(second) - DateTime.Now;
            }
            else if (second == -1)
            {
            }
            else
            {
                throw new BusinessException("过期时间设置有误", HttpStatus.Err.Id);
            }

            return timeSpan;
        }

        #endregion

        #endregion 辅助方法

        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 99;
        }

        #endregion

        #region private methods

        #region 清理敏感字符

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetKey(string key)
        {
            key = key.Replace("！", "!");
            return key;
        }

        #endregion

        #endregion
    }
}
