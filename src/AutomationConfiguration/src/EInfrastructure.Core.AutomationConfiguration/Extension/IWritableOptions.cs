using System;
using Microsoft.Extensions.Options;

namespace EInfrastructure.Core.AutomationConfiguration.Extension
{
    /// <summary>
    /// 更改配置文件的读写接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        /// <summary>
        /// 得到配置
        /// </summary>
        /// <returns></returns>
        T Get();

        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        TSource Get<TSource>() where TSource : class, new();

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="applyChanges"></param>
        void Update(Action<T> applyChanges);

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="applyChanges"></param>
        void Update<TSource>(Action<TSource> applyChanges)
            where TSource : class, new();
    }
}
