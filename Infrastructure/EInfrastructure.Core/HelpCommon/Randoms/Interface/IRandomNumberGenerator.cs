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
        int Generate(int min, int max);
    }
}