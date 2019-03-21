using EInfrastructure.Core.Exception;
using EInfrastructure.Core.Interface.Storage.Enum;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using Qiniu.Storage;

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
        public string UserAgent { get; set; }

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
        public string RsHost { get; set; }

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
        public string RsfHost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PrefetchHost { get; set; }

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
        /// 上传成功后，七牛云向业务服务器发送 POST 请求的 URL
        /// 可以使用ip，减少对dns的依赖
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// 回调七牛Host（可为空,非真正回调地址）
        /// </summary>
        public string CallbackHost { get; set; } = null;

        /// <summary>
        /// 回调内容
        /// </summary>
        public string CallbackBody { get; set; } =
            "{\"key\":\"$(key)\",\"hash\":\"$(etag)\",\"fsiz\":$(fsize),\"bucket\":\"$(bucket)\",\"name\":\"$(x:name)\",\"mimeType\":\"$(mimeType)\"}";

        /// <summary>
        /// 回调内容类型
        /// </summary>
        public CallbackBodyTypeEnum CallbackBodyType { get; set; } = CallbackBodyTypeEnum.Json;

        /// <summary>
        /// 鉴权回调
        /// </summary>
        public string CallbackAuthHost { get; set; }

        /// <summary>
        /// 得到空间
        /// </summary>
        /// <returns></returns>
        public Zone GetZone()
        {
            switch (Zones)
            {
                default:
                    return Zone.ZONE_CN_East;
                case ZoneEnum.ZoneCnNorth:
                    return Zone.ZONE_CN_North;
                case ZoneEnum.ZoneCnSouth:
                    return Zone.ZONE_CN_South;
                case ZoneEnum.ZoneUsNorth:
                    return Zone.ZONE_US_North;
            }
        }

        /// <summary>
        /// 是否第一次获取
        /// </summary>
        private static bool IsFirst = true;

        /// <summary>
        /// 存储配置
        /// </summary>
        private static QiNiuConfig Config = new QiNiuConfig();

        /// <summary>
        /// 设置七牛存储配置
        /// </summary>
        /// <param name="qiNiuConfig"></param>
        internal void Set()
        {
            Config = this;
        }

        /// <summary>
        /// 读取七牛存储配置
        /// </summary>
        /// <returns></returns>
        internal static QiNiuConfig Get()
        {
            if (Config.Equals(new QiNiuConfig()) && !IsFirst)
            {
                throw new BusinessException("未配置七牛");
            }

            IsFirst = false;
            return Config;
        }
    }
}