using System.Xml.Serialization;

namespace EInfrastructure.Core.AliYun.DaYu.Model
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot]
    public class SendSmsResponse
    {
        [XmlElement]
        public string Message { get; set; }

        [XmlElement]
        public string RequestId { get; set; }

        [XmlElement]
        public string BizId { get; set; }

        [XmlElement]
        public string Code { get; set; }
    }
}
