using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    /// 短链接
    /// </summary>
    public interface IShortenProvider : ISingleInstance, IIdentify
    {
        /// <summary>
        /// 得到短参数信息
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="key">md5盐（默认）</param>
        /// <param name="number">生成短连接的长度</param>
        /// <returns>得到短参数的值（有四个，任选其一即可）</returns>
        string[] GetShortParam(string param, string key, int number = 6);
    }
}
