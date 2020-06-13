using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Bucket
{
    /// <summary>
    /// 空间权限
    /// </summary>
    public class BucketPermissItemResultDto:OperateResultDto
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="permiss">权限</param>
        /// <param name="msg">消息</param>
        public BucketPermissItemResultDto(bool state,Permiss permiss, string msg) : base(state, msg)
        {
            Permiss = permiss;
        }


        /// <summary>
        /// 空间访问权限 公开：0 私有：1 公共读写：2
        /// </summary>
        public Permiss Permiss { get; private set; }
    }
}
