namespace EInfrastructure.Core.UCloud.Storage.Config
{
    public class UFileError
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
        private int RetCode;
        
        /// <summary>
        /// 错误信息
        /// </summary>
        private string ErrMsg;
    }
}