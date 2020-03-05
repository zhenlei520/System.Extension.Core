using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Http.Enumerations
{
    /// <summary>
    /// Http Post请求Body类型
    /// </summary>
    public class RequestBodyType : Enumeration
    {

        /// <summary>
        /// application/json
        /// </summary>
        public static RequestBodyType ApplicationJson = new RequestBodyType(1, "application/json");

        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        public static RequestBodyType ApplicationXWwwFormUrlencoded = new RequestBodyType(2, "application/x-www-form-urlencoded");

        /// <summary>
        /// multipart/form-data
        /// </summary>
        public static RequestBodyType MultipartFormData = new RequestBodyType(3, "multipart/form-data");

        /// <summary>
        /// text/xml
        /// </summary>
        public static RequestBodyType TextXml = new RequestBodyType(4, "text/xml");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public RequestBodyType(int id, string name) : base(id, name)
        {
        }
    }
}
