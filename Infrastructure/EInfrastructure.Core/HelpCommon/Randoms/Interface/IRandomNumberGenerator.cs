using EInfrastructure.Core.Interface.IOC;

namespace EInfrastructure.Core.HelpCommon.Randoms.Interface
{
    /// <summary>
    /// 随机数字生成器
    /// </summary>
    public interface IRandomNumberGenerator : ISingleInstance
    {
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="isHighPerformance">是否高性能</param>
        int Generate(int min, int max, bool isHighPerformance = false);

        /// <summary>
        /// 生成指定位数的随机数字
        /// </summary>
        /// <param name="num">长度</param>
        /// <returns></returns>
        int Generate(int num);
    }
}