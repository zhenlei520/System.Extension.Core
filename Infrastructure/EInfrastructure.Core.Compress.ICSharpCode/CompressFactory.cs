using EInfrastructure.Core.Exception;
using EInfrastructure.Core.Interface.Compress;
using EInfrastructure.Core.Interface.Compress.Enum;

namespace EInfrastructure.Core.Compress.ICSharpCode
{
    public class CompressFactory
    {
        #region 得到实现类

        /// <summary>
        /// 得到实现类
        /// </summary>
        /// <param name="compressType">压缩方式</param>
        /// <returns></returns>
        internal static ICompressService GetProvider(CompressTypeEnum compressType = CompressTypeEnum.Zip)
        {
            switch (compressType)
            {
                case CompressTypeEnum.Zip:
                    return new CompressService();
                default:
                    throw new BusinessException("暂不支持的压缩方式");
            }
        }

        #endregion
    }
}