using EInfrastructure.Core.QiNiu.Storage.Enum;

namespace EInfrastructure.Core.QiNiu.Storage.Config
{
    /// <summary>
    /// 七牛配置
    /// </summary>
    public class QiNiuConfig
    {
        /// <summary>
        /// 代理
        /// </summary>
        public string UserAgent { get; protected set; }

        /// <summary>
        /// 七牛提供的公钥，用于识别用户
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// 七牛提供的秘钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 七牛资源管理服务器地址
        /// </summary>
        public string RsHost { get; protected set; }

        /// <summary>
        /// 七牛资源上传服务器地址.
        /// </summary>
        public string UpHost
        {
            get
            {
                switch (Zones)
                {
                    case ZoneEnum.ZoneCnEast:
                    default:
                        return "http://upload.qiniup.com";
                    case ZoneEnum.ZoneCnNorth:
                        return "http://upload-z1.qiniup.com";
                    case ZoneEnum.ZoneCnSouth:
                        return "http://upload-z2.qiniup.com";
                    case ZoneEnum.ZoneUsNorth:
                        return "http://upload-na0.qiniup.com";
                }
            }
        }

        /// <summary>
        /// 空间
        /// </summary>
        public ZoneEnum Zones { get; set; }

        /// <summary>
        /// 七牛资源列表服务器地址.
        /// </summary>
        public string RsfHost { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public string PrefetchHost { get; protected set; }

        /// <summary>
        /// Api域
        /// </summary>
        public string ApiHost { get; protected set; }

        /// <summary>
        /// 对外访问的主机名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 存储的空间名
        /// </summary>
        public string Bucket { get; set; }

        /// <summary>
        /// 传输队列
        /// </summary>
        public string PersistentPipeline { get; set; }

        /// <summary>
        /// 持久化结果通知
        /// </summary>
        public string PersistentNotifyUrl { get; set; }

        /// <summary>
        /// 回调内容
        /// </summary>
        public string CallbackBody { get; set; }

        /// <summary>
        /// 回调内容类型
        /// </summary>
        public string CallbackBodyType { get; set; } = "application/json";

    }
}
