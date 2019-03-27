namespace EInfrastructure.Core.UCloud.Storage.Config
{
    /// <summary>
    /// 
    /// </summary>
    internal class UFileError
    {
        /// <summary>
        /// 得到code码
        /// </summary>
        /// <returns></returns>
        internal int GetRetCode()
        {
            return RetCode;
        }

        /// <summary>
        /// 得到错误信息
        /// </summary>
        /// <returns></returns>
        internal string GetErrMsg()
        {
            return ErrMsg;
        }

        /// <summary>
        /// 错误码
        /// </summary>
        internal int RetCode { private get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        internal string ErrMsg { private get; set; }
    }
}