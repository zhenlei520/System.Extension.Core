using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Cache;
using EInfrastructure.Core.Interface.IOC;
using EInfrastructure.Core.Redis.Common;
using EInfrastructure.Core.Redis.Config;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Redis
{
    /// <summary>
    /// Redis缓存服务
    /// </summary>
    public class RedisCacheService : ICacheService, ISingleInstance
    {
        /// <summary>
        /// 过期的Hash key
        /// </summary>
        private readonly string _overtimeCacheKey = "Cache_HashKey";

        /// <summary>
        /// 前缀
        /// </summary>
        private readonly string _prefix;

        /// <summary>
        /// 
        /// </summary>
        public RedisCacheService()
        {
            var redisConfig = RedisConfig.Get();
            _prefix = redisConfig.Name;
            CsRedisHelper.InitializeConfiguration(redisConfig);
        }

        #region Methods

        #region String

        #region 同步方法

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return CsRedisHelper.Set(key, value, expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1);
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            return CsRedisHelper.Set(key, ConvertJson(obj),
                expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            return CsRedisHelper.Get(key);
        }

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKeys">Redis Key集合</param>
        /// <returns></returns>
        public List<string> StringGet(List<string> listKeys)
        {
            return Enumerable.ToList<string>(CsRedisHelper.GetStrings(listKeys.ToArray()));
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            return ConvertObj<T>(key);
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public long StringIncrement(string key, long val = 1)
        {
            return CsRedisHelper.Increment(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public long StringDecrement(string key, long val = 1)
        {
            return CsRedisHelper.Increment(key, 0 - val);
        }

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return await CsRedisHelper.SetAsync(key, value,
                expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1);
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            return await CsRedisHelper.SetAsync(key, ConvertJson<T>(obj),
                expiry.HasValue ? Convert.ToInt32(expiry.Value.TotalSeconds) : -1);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string key)
        {
            return await CsRedisHelper.GetAsync(key);
        }


        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<long> StringIncrementAsync(string key, long val = 1)
        {
            return await CsRedisHelper.IncrementAsync(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<long> StringDecrementAsync(string key, long val = 1)
        {
            return await CsRedisHelper.IncrementAsync(key, 0 - val);
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
            return CsRedisHelper.HashExists(key, dataKey);
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string dataKey, T t, long second = -1)
        {
            string value = CsRedisHelper.HashSet(key, GetExpire(second), dataKey, ConvertJson<T>(t));
            bool result = string.Equals(value, "OK",
                StringComparison.OrdinalIgnoreCase);
            return result;
        }

        /// <summary>
        ///  存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="kvalues"></param>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public bool HashSet<T>(string key, Dictionary<string, T> kvalues, long second = -1)
        {
            List<object> keyValues = new List<object>();
            foreach (var kvp in kvalues)
            {
                keyValues.Add(kvp.Key);
                keyValues.Add(ConvertJson<T>(kvp.Value));
            }

            return string.Equals(CsRedisHelper.HashSet(key, GetExpire(second), keyValues.ToArray()), "OK",
                StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string dataKey)
        {
            return CsRedisHelper.HashDelete(key, dataKey) >= 0;
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long HashDelete(string key, List<string> dataKeys)
        {
            return CsRedisHelper.HashDelete(key, dataKeys.ToArray());
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string dataKey)
        {
            return ConvertObj<T>(CsRedisHelper.HashGet(key, dataKey));
        }


        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public Dictionary<string, string> HashGet(string key, List<string> dataKeys)
        {
            dataKeys = dataKeys.Distinct().ToList();
            List<string> values = Enumerable.ToList<string>(CsRedisHelper.HashGet(key, dataKeys.ToArray()));

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

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public long HashIncrement(string key, string dataKey, long val = 1)
        {
            return CsRedisHelper.HashIncrement(key, dataKey, val);
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
            return CsRedisHelper.HashIncrement(key, dataKey, 0 - val);
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> HashKeys(string key)
        {
            return Enumerable.ToList<string>(CsRedisHelper.HashKeys(key));
        }

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string key, string dataKey)
        {
            return await CsRedisHelper.HashExistsAsync(key, dataKey);
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string key, string dataKey, T t)
        {
            return string.Equals(await CsRedisHelper.HashSetAsync(key, dataKey, ConvertJson<T>(t)), "TRUE",
                StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string dataKey)
        {
            return await CsRedisHelper.HashDeleteAsync(key, dataKey) >= 0;
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string key, List<string> dataKeys)
        {
            return await CsRedisHelper.HashDeleteAsync(key, dataKeys.ToArray());
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string dataKey)
        {
            return ConvertObj<T>(await CsRedisHelper.HashGetAsync(key, dataKey));
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<long> HashIncrementAsync(string key, string dataKey, long val = 1)
        {
            return await CsRedisHelper.HashIncrementAsync(key, dataKey, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<long> HashDecrementAsync(string key, string dataKey, long val = 1)
        {
            return await CsRedisHelper.HashIncrementAsync(key, dataKey, 0 - val);
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<string>> HashKeysAsync(string key)
        {
            return Enumerable.ToList<string>((await CsRedisHelper.HashKeysAsync(key)));
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
            return CsRedisHelper.LRem(key, int.MaxValue, ConvertJson<T>(value));
        }

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> ListRange<T>(string key, long count = 1000)
        {
            List<T> list = new List<T>();
            Enumerable.ToList<string>(CsRedisHelper.LRang(key, 0, count)).ForEach(p => { list.Add(ConvertObj<T>(p)); });
            return list;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string key, T value)
        {
            return CsRedisHelper.RPush(key, new string[1] {ConvertJson<T>(value)});
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            return ConvertObj<T>(CsRedisHelper.RPop(key));
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string key, T value)
        {
            return CsRedisHelper.LPush(key, new string[1] {ConvertJson<T>(value)});
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            return ConvertObj<T>(CsRedisHelper.LPop(key));
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return CsRedisHelper.LLen(key);
        }

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRemoveAsync<T>(string key, T value)
        {
            return await CsRedisHelper.LRemAsync(key, int.MaxValue, ConvertJson<T>(value));
        }

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<T>> ListRangeAsync<T>(string key, long count = 1000)
        {
            return ConvertListObj<T>(Enumerable.ToList<string>((await CsRedisHelper.LRangAsync(key, 0, count))));
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, T value)
        {
            return await CsRedisHelper.RPushAsync(key, new string[1] {ConvertJson<T>(value)});
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string key)
        {
            return ConvertObj<T>(await CsRedisHelper.RPopAsync(key));
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string key, T value)
        {
            return await CsRedisHelper.LPushAsync(key, new string[1] {ConvertJson<T>(value)});
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            return ConvertObj<T>(await CsRedisHelper.LPopAsync(key));
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string key)
        {
            return await CsRedisHelper.LLenAsync(key);
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

            return CsRedisHelper.ZAdd(key, (score, ConvertJson<T>(value))) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SortedSetRemove<T>(string key, T value)
        {
            return CsRedisHelper.ZRem(key, new string[1] {ConvertJson<T>(value)}) > 0;
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> SortedSetRangeByRank<T>(string key, long count = 1000)
        {
            return ConvertListObj<T>(CsRedisHelper.ZRange(key, 0, count).ToList<string>());
        }

        /// <summary>
        /// 获取已过期的hashKey
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Dictionary<string, string> SortedSetRangeByRankAndOverTime(long count = 1000)
        {
            var keyList = CsRedisHelper
                .ZRevRangeByScore(_overtimeCacheKey, TimeCommon.GetTimeSpan(DateTime.Now), 0, count, null)
                .ToList<string>(); //得到过期的key集合

            Dictionary<string, string> hasKey = new Dictionary<string, string>();
            keyList.ForEach(item =>
            {
                var keys = item.Replace("~_~", "!").Split('!');
                if (!hasKey.ContainsKey(keys[0]))
                {
                    hasKey.Add(keys[0], keys[1]);
                }
            });
            return hasKey;
        }

        /// <summary>
        /// 降序获取指定索引的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        public List<T> GetRangeFromSortedSetDesc<T>(string key, long fromRank, long toRank)
        {
            return ConvertListObj<T>(Enumerable.ToList<string>(CsRedisHelper.ZRevRange(key, fromRank, toRank)));
        }

        /// <summary>
        /// 获取指定索引的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fromRank"></param>
        /// <param name="toRank"></param>
        /// <returns></returns>
        public List<T> GetRangeFromSortedSet<T>(string key, long fromRank, long toRank)
        {
            return ConvertListObj<T>(Enumerable.ToList<string>(CsRedisHelper.ZRange(key, fromRank, toRank)));
        }

        /// <summary>
        /// 判断是否存在项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SortedSetExistItem<T>(string key, T value)
        {
            return CsRedisHelper.ZScore(key, ConvertJson<T>(value)).HasValue;
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            return CsRedisHelper.ZCard(key);
        }

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
            return await CsRedisHelper.ZAddAsync(key, (score, ConvertJson<T>(value))) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            return await CsRedisHelper.ZRemAsync(key, new string[1] {ConvertJson<T>(value)}) > 0;
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<T>> SortedSetRangeByRankAsync<T>(string key, long count = 1000)
        {
            return ConvertListObj<T>(Enumerable.ToList<string>((await CsRedisHelper.ZRangeAsync(key, 0, count))));
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string key)
        {
            return await CsRedisHelper.ZCardAsync(key);
        }

        #endregion 异步方法

        #endregion SortedSet 有序集合

        #region Basics

        #region  删除指定Key的缓存    

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns>返回删除的数量</returns>
        public long Remove(List<string> keys)
        {
            return CsRedisHelper.Remove(keys?.ToArray());
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
            return CsRedisHelper.Exists(key);
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
            return CsRedisHelper.Expire(key, expire);
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
            CsRedisHelper.Keys(_prefix + pattern).ToList().ForEach(p => { keys.Add(p.Substring(_prefix.Length)); });
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
            string result = value is string ? value.ToString() : JsonConvert.SerializeObject(value);
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
        public List<T> ConvertListObj<T>(List<string> values)
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
        private T ConvertObj<T>(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(value);
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
            TimeSpan timeSpan;
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
                throw new BusinessException("过期时间设置有误");
            }

            return timeSpan;
        }

        #endregion

        #endregion 辅助方法
    }
}