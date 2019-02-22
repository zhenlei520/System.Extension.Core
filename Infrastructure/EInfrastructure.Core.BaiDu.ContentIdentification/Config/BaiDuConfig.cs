using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.BaiDu.ContentIdentification.Config
{
    /// <summary>
    /// 百度鉴黄配置
    /// </summary>
    public class BaiDuConfig
    {
        /// <summary>
        /// 百度鉴定信息
        /// </summary>
        private static BaiDuConfig Config;

        /// <summary>
        /// 设置百度鉴定信息
        /// </summary>
        internal void Set()
        {
            Config = this;
        }

        /// <summary>
        /// 读取百度鉴定信息
        /// </summary>
        /// <returns></returns>
        internal static BaiDuConfig Get()
        {
            if (Config == null)
            {
                throw new BusinessException("未配置百度鉴定信息");
            }

            return Config;
        }
    }
}