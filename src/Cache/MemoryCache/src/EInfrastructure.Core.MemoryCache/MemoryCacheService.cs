// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using EInfrastructure.Core.Config.CacheExtensions;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.HelpCommon;
using Microsoft.Extensions.Caching.Memory;

namespace EInfrastructure.Core.MemoryCache
{
    /// <summary>
    /// MemoryCache缓存实现类
    /// </summary>
    public class MemoryCacheService : ICacheService, ISingleInstance
    {
        private readonly IMemoryCache _cache;

        /// <summary>
        ///
        /// </summary>
        /// <param name="cache"></param>
        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
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

        #region String

        #region 同步方法

        #region 保存单个key value

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return StringSet<string>(key, value, expiry);
        }

        #endregion

        #region 保存一个对象

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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (expiry != null)
            {
                _cache.Set(key, obj,
                    new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(expiry.Value)
                );
            }
            else
            {
                _cache.Set(key, obj);
            }

            return true;
        }

        #endregion

        #region 获取单个key的值

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.Get<string>(key);
        }

        #endregion

        #region 获取多个Key

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKeys">Redis Key集合</param>
        /// <returns></returns>
        public List<string> StringGet(List<string> listKeys)
        {
            List<string> results = new List<string>();
            foreach (var key in listKeys)
            {
                results.Add(StringGet(key));
            }

            return results;
        }

        #endregion

        #region 获取一个key的对象

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key) where T : class, new()
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.Get<T>(key);
        }

        #endregion

        #region 为数字增长val

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public long StringIncrement(string key, long val = 1)
        {
            long result = 0;
            var value = StringGet(key);
            if (string.IsNullOrEmpty(value))
            {
                result = result + val;
                StringSet(key, val);
            }
            else
            {
                result = value.ConvertToInt(0) + val;
                StringSet(key, result);
            }

            return result;
        }

        #endregion

        #region 为数字减少val

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public long StringDecrement(string key, long val = 1)
        {
            long result = 0;
            var value = StringGet(key);
            if (string.IsNullOrEmpty(value))
            {
                result = result - val;
                StringSet(key, val);
            }
            else
            {
                result = value.ConvertToInt(0) - val;
                StringSet(key, result);
            }

            return result;
        }

        #endregion

        #endregion 同步方法

        #region 异步方法

        #region 保存单个key value

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return new Task<bool>(() => StringSet(key, value, expiry));
        }

        #endregion

        #region 保存一个对象

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            return new Task<bool>(() => StringSet(key, obj, expiry));
        }

        #endregion

        #region 获取单个key的值

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public Task<string> StringGetAsync(string key)
        {
            return new Task<string>(() => StringGet(key));
        }

        #endregion

        #region 为数字增长val

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public Task<long> StringIncrementAsync(string key, long val = 1)
        {
            return new Task<long>(() => StringIncrement(key, val));
        }

        #endregion

        #region 为数字减少val

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public Task<long> StringDecrementAsync(string key, long val = 1)
        {
            return new Task<long>(() => StringDecrement(key, val));
        }

        #endregion

        #endregion 异步方法

        #endregion String

        #region Hash

        #region 同步方法

        #region 判断某个数据是否已经被缓存

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            return false;
        }

        #endregion

        #region 存储数据到hash表

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string dataKey, T t, long second = -1L, bool isSetHashKeyExpire = true)
        {
            return false;
        }

        #endregion

        #region 存储数据到hash表

        /// <summary>
        ///  存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="kvalues"></param>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public bool HashSet<T>(string key, Dictionary<string, T> kvalues, long second = -1L,
            bool isSetHashKeyExpire = true)
        {
            return false;
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <param name="kValues"></param>
        /// <param name="second"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HashSet<T>(Dictionary<string, Dictionary<string, T>> kValues, long second = -1,
            bool isSetHashKeyExpire = true)
        {
            return false;
        }

        #region 清除过期的hashkey(自定义hashkey删除)

        /// <summary>
        /// 清除过期的hashkey(自定义hashkey删除)
        /// </summary>
        /// <param name="count">指定清除指定数量的已过期的hashkey</param>
        /// <returns></returns>
        public bool ClearOverTimeHashKey(long count = 1000l)
        {
            return false;
        }

        #endregion

        #endregion

        #region 移除hash中的某值

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string dataKey)
        {
            return false;
        }

        #endregion

        #region 移除hash中的多个值

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long HashDelete(string key, List<string> dataKeys)
        {
            return 0;
        }

        #endregion

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
            return default(T);
        }


        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public string HashGet(string key, string dataKey)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 从hash表获取数据

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public Dictionary<string, string> HashGet(string key, List<string> dataKeys)
        {
            return null;
        }

        public Dictionary<string, Dictionary<string, string>> HashGet(Dictionary<string, string[]> keys)
        {
            return null;
        }

        #endregion

        #region 为数字增长val

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public long HashIncrement(string key, string dataKey, long val = 1)
        {
            return 0;
        }

        #endregion

        #region 为数字减少val

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public long HashDecrement(string key, string dataKey, long val = 1)
        {
            return 0;
        }

        #endregion

        #region 获取hashkey所有Redis key

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> HashKeys(string key)
        {
            return null;
        }

        #endregion

        #endregion 同步方法

        #region 异步方法

        #region 判断某个数据是否已经被缓存

        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Task<bool> HashExistsAsync(string key, string dataKey)
        {
            return new Task<bool>(() => HashExists(key, dataKey));
        }

        #endregion

        #region 存储数据到hash表

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public Task<bool> HashSetAsync<T>(string key, string dataKey, T t)
        {
            return new Task<bool>(() => HashSet(key, dataKey, t));
        }

        #endregion

        #region 移除hash中的某值

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Task<bool> HashDeleteAsync(string key, string dataKey)
        {
            return new Task<bool>(() => HashDelete(key, dataKey));
        }

        #endregion

        #region 移除hash中的多个值

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public Task<long> HashDeleteAsync(string key, List<string> dataKeys)
        {
            return new Task<long>(() => HashDelete(key, dataKeys));
        }

        #endregion

        #region 从hash表获取数据

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Task<T> HashGetAsync<T>(string key, string dataKey) where T : class, new()
        {
            return new Task<T>(() => HashGet<T>(key, dataKey));
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public Task<string> HashGetAsync(string key, string dataKey)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 为数字增长val

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public Task<long> HashIncrementAsync(string key, string dataKey, long val = 1)
        {
            return new Task<long>(() => HashIncrement(key, dataKey, val));
        }

        #endregion

        #region 为数字减少val

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public Task<long> HashDecrementAsync(string key, string dataKey, long val = 1)
        {
            return new Task<long>(() => HashDecrement(key, dataKey, val));
        }

        #endregion

        #region 获取hashkey所有Redis key

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<List<string>> HashKeysAsync(string key)
        {
            return new Task<List<string>>(() => HashKeys(key));
        }

        #endregion

        #endregion 异步方法

        #endregion Hash

        #region List

        #region 同步方法

        #region 移除指定ListId的内部List的值

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRemove<T>(string key, T value)
        {
            return 0;
        }

        public List<string> ListRange(string key, long count = 1000)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取指定key的List

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> ListRange<T>(string key, long count = 1000L) where T : class, new()
        {
            return default(List<T>);
        }

        #endregion

        #region 入队

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string key, T value)
        {
            return 0;
        }

        public string ListRightPop(string key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 出队

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key) where T : class, new()
        {
            return default(T);
        }

        #endregion

        #region 入栈

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string key, T value)
        {
            return 0;
        }

        public string ListLeftPop(string key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 出栈

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key) where T : class, new()
        {
            return default(T);
        }

        #endregion

        #region 获取集合中的数量

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return 0;
        }

        #endregion

        #endregion 同步方法

        #region 异步方法

        #region 移除指定ListId的内部List的值

        /// <summary>
        /// 移除指定ListId的内部List的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> ListRemoveAsync<T>(string key, T value)
        {
            return new Task<long>(() => ListRemove(key, value));
        }

        public Task<List<string>> ListRangeAsync(string key, long count = 1000)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取指定key的List

        /// <summary>
        /// 获取指定key的List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Task<List<T>> ListRangeAsync<T>(string key, long count = 1000L) where T : class, new()
        {
            return new Task<List<T>>(() => ListRange<T>(key, count));
        }

        #endregion

        #region 入队

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> ListRightPushAsync<T>(string key, T value)
        {
            return new Task<long>(() => ListRightPush<T>(key, value));
        }

        public Task<string> ListRightPopAsync(string key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 出队

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T> ListRightPopAsync<T>(string key) where T : class, new()
        {
            return new Task<T>(() => ListRightPop<T>(key));
        }

        #endregion

        #region 入栈

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<long> ListLeftPushAsync<T>(string key, T value)
        {
            return new Task<long>(() => ListLeftPush<T>(key, value));
        }

        public Task<string> ListLeftPopAsync(string key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 出栈

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T> ListLeftPopAsync<T>(string key) where T : class, new()
        {
            return new Task<T>(() => ListLeftPop<T>(key));
        }

        #endregion

        #region 获取集合中的数量

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<long> ListLengthAsync(string key)
        {
            return new Task<long>(() => ListLength(key));
        }

        #endregion

        #endregion 异步方法

        #endregion List

        #region SortedSet 有序集合

        #region 同步方法

        #region 添加 (当score一样value一样时不插入)

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
            return false;
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SortedSetRemove<T>(string key, T value)
        {
            return false;
        }

        public List<string> SortedSetRangeByRank(string key, long count = 1000)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取全部

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> SortedSetRangeByRank<T>(string key, long count = 1000L) where T : class, new()
        {
            return default(List<T>);
        }

        #endregion

        #region 降序获取指定索引的集合

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
            return default(List<T>);
        }

        public List<string> GetRangeFromSortedSet(string key, long fromRank, long toRank)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取已过期的hashKey

        /// <summary>
        /// 获取已过期的hashKey
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ValueTuple<string, string, string, string>> SortedSetRangeByRankAndOverTime(long count = 1000l)
        {
            return default(List<ValueTuple<string, string, string, string>>);
        }

        #endregion

        #region 获取指定索引的集合

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
            return default(List<T>);
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
            return false;
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
            return 0;
        }

        #endregion

        #endregion 同步方法

        #region 异步方法

        #region 添加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public Task<bool> SortedSetAddAsync<T>(string key, T value, double score)
        {
            return new Task<bool>(() => SortedSetAdd(key, value, score));
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            return new Task<bool>(() => SortedSetRemove(key, value));
        }

        public Task<List<string>> SortedSetRangeByRankAsync(string key, long count = 1000)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 获取全部

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Task<List<T>> SortedSetRangeByRankAsync<T>(string key, long count = 1000L) where T : class, new()

        {
            return new Task<List<T>>(() => SortedSetRangeByRank<T>(key, count));
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
            return new Task<long>(() => SortedSetLength(key));
        }

        #endregion

        #endregion 异步方法

        #endregion SortedSet 有序集合

        #region Basics

        #region  删除指定Key的缓存

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合</param>
        /// <returns>返回删除的数量</returns>
        public long Remove(List<string> keys)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除指定Key的缓存
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="keys">待删除的Key集合，不含prefix前辍RedisHelper.Name</param>
        /// <returns>返回删除的数量</returns>
        public long Remove(params string[] keys)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 检查给定 key 是否存在

        /// <summary>
        /// 检查给定 key 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exist(string key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 设置指定key过期时间

        /// <summary>
        /// 设置指定key过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan expire)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 查找所有符合给定模式( pattern)的 key

        /// <summary>
        /// 查找所有符合给定模式( pattern)的 key
        /// </summary>
        /// <param name="pattern">如：runoob*</param>
        /// <returns></returns>
        public List<string> Keys(string pattern)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}
