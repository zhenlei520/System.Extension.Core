namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage
{
    /// <summary>
    /// 得到访问信息
    /// </summary>
    public class GetVisitUrlResultDto : OperateResultDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url">访问url</param>
        /// <param name="msg"></param>
        public GetVisitUrlResultDto(string url, string msg) : base(true, msg)
        {
            Url = url;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="msg"></param>
        public GetVisitUrlResultDto(string msg) : base(false, msg)
        {
        }

        /// <summary>
        /// 访问url
        /// </summary>
        public string Url { get; }
    }
}
